using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace K_Means
{
    static class Program
    {
        public static System.Data.SqlClient.SqlDataReader reader;
        public static SqlConnection connection = new SqlConnection();
        public static String servername = "DESKTOP-UPGKA2J"; // luu ten server tra vè ở form dang nhap
        public static String username = "sa";
        public static String password = "123";
        public static Boolean check = true;
        public static String database = "KMEAN";
        public static String tablename = "Medicine";
        internal static object sqlcmd;
        public static SqlDataReader myReader;
        public static List<string> kqTimKiem = new List<string>();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Program.KetNoi() == 0) return;

            String strLenh = "select name from sys.columns where object_id = OBJECT_ID('" + tablename + "') ";
            Program.myReader = Program.ExecSqlDataReader(strLenh);
            while (Program.myReader.Read())
            {
                kqTimKiem.Add(Program.myReader.GetString(0).Trim());
            }
            Application.Run(new Form1());
        }
        public static SqlDataReader ExecSP(String lenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(lenh, Program.connection);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 400;
            if (Program.connection.State == ConnectionState.Closed) Program.connection.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                Program.connection.Close();
                MessageBox.Show(ex.Message, "Exec");
                return null;
            }
        }
        public static int KetNoi()
        {
            if (Program.connection != null && Program.connection.State == ConnectionState.Open)
                Program.connection.Close();
            try
            {
                String cntstr = "Data Source=" + Program.servername + ";Initial Catalog=" +
                      Program.database + ";User ID=" +
                      Program.username + ";password=" + Program.password;
                Program.connection.ConnectionString = cntstr;
                Program.connection.Open();
                return 1;
            }

            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu.\nBạn xem lại user name và password.\n " + e.Message, "Kết nối", MessageBoxButtons.OK);
                return 0;
            }
        }
        public static int ExecSqlNonQuery(String strlenh)
        {

            if (connection.State == ConnectionState.Closed)
            {
                KetNoi();
            }
            SqlCommand Sqlcmd = new SqlCommand(strlenh, connection);
            Sqlcmd.CommandType = CommandType.Text;
            Sqlcmd.CommandTimeout = 600;// 10 phut 
            try
            {
                Sqlcmd.ExecuteNonQuery();
                return 0;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
                return ex.State; // trang thai lỗi gởi từ RAISERROR trong SQL Server qua
            }
        }
        public static SqlDataReader ExecSqlDataReader(String strLenh)
        {
            SqlDataReader myreader;
            SqlCommand sqlcmd = new SqlCommand(strLenh, Program.connection);
            sqlcmd.CommandType = CommandType.Text;
            //tối đa cho đợi 10p, tgian tính bằng s
            sqlcmd.CommandTimeout = 600;
            // Kiểm tra trạng thái đóng hay mở
            if (Program.connection.State == ConnectionState.Closed) Program.connection.Open();
            try
            {
                myreader = sqlcmd.ExecuteReader();
                return myreader;
            }
            catch (SqlException ex)
            {
                Program.connection.Close();
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static DataTable ExecSqlDataTable(String cmd)
        {
            // Trả về datable
            DataTable dt = new DataTable();
            //Nếu đang đóng thì mở
            if (Program.connection.State == ConnectionState.Closed) Program.connection.Open();
            // Muốn gọi csdl phải thông qua SqlDataAdapter
            SqlDataAdapter da = new SqlDataAdapter(cmd, connection);
            // Chạy lệnh cmd
            da.Fill(dt);
            connection.Close();
            return dt;
        }
        public static int[] Cluster(double[][] rawData, int numClusters)
        {
            double[][] data = Normalized(rawData);
            String inputdata = "";
            Debug.WriteLine("\n1. INPUT DATA");
            for (int i = 0; i < rawData.Length; ++i)//dòng
            {
                for (int j = 0; j < rawData[0].Length; j++)
                {
                    inputdata += rawData[i][j] + ", ";
                }
                inputdata += "\n";
            }
            Debug.WriteLine(inputdata);
            bool changed = true;
            bool success = true;
            int[] clustering = Init(data.Length, numClusters, 0);
            clustering = InitClustering(data, numClusters, clustering);
            double[][] means = Allocate(numClusters, data[0].Length);

            var distinctList = data.GroupBy(x => string.Join(",", x))
                                 .Select(g => g.First())
                                 .ToList();
            for (int i = 0; i < numClusters; ++i)
            {
                means[i] = new double[distinctList[i].Length];
                Array.Copy(distinctList[i], means[i], distinctList[i].Length);
            }
            UpdateMeans(data, clustering, means);
            int maxCount = data.Length * 10;
            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct;
                if (ct > 0)
                    success = UpdateMeans(data, clustering, means);
                changed = UpdateClustering(data, clustering, means);
            }
            return clustering;
        }

        private static double[][] Normalized(double[][] rawData)
        {
            double[][] result = new double[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new double[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }
            for (int j = 0; j < result[0].Length; ++j) 
            {
                double colSum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    colSum += result[i][j];
                double mean = colSum / result.Length;
                double sum = 0.0;
                for (int i = 0; i < result.Length; ++i)
                    sum += (result[i][j] - mean) * (result[i][j] - mean);
                double sd = Math.Sqrt(sum / result.Length);
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }
        private static int[] Init(int numData, int numClusters, int seeds)
        {
            Random random = new Random(seeds);
            int[] clustering = new int[numData];
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = 0;// random.Next(0, numClusters);//tạo 1 số ngấu nhiên nằm trong khoảng 0 đến số lượng cụm
            return clustering;
        }

        private static int[] InitClustering(double[][] data, int numClusters, int[] clustering)
        {
            int num = numClusters;
            int[] newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);
            double[] distances = new double[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                {
                    distances[k] = Distance(data[i], data[k]);
                }
                int newClusterID = MinIndex(distances);
                if (newClusterID != newClustering[i])
                {
                    newClustering[i] = newClusterID;
                }
            }
            return newClustering;
        }

        private static double[][] Allocate(int numClusters, int numCols)
        {
            double[][] result = new double[numClusters][];
            for (int k = 0; k < numClusters; ++k)
                result[k] = new double[numCols];
            return result;
        }

        private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];// đếm số lượng phần từ được thêm vào cụm cluster
            }
            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < data[i].Length; ++j)
                {
                    means[cluster][j] += data[i][j];
                }
            }
            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                {
                    means[k][j] /= clusterCounts[k];
                }
            return true;
        }

        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
        {
            int numClusters = means.Length;
            bool changed = false;
            int[] newClustering = new int[clustering.Length];
            Array.Copy(clustering, newClustering, clustering.Length);
            double[] distances = new double[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                {
                    distances[k] = Distance(data[i], means[k]);
                }
                int newClusterID = MinIndex(distances);
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }
            if (changed == false)
                return false;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }
            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false;
            Array.Copy(newClustering, clustering, newClustering.Length);
            return true;
        }

        private static double Distance(double[] tuple, double[] mean)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }
    }
}