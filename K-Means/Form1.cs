using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace K_Means
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        double[][] arrayOfDoubles;
        public static String SYS_DB = "";
        public static String SYS_TABLE = "";
        String[] pointname = null;
        public static DataTable dt = null;
        double[,] point = null;
        String[,] rs = null;
        private Random rnd = new Random();
        public string[] colnames;
        public string[] meanResults;
        public Form1()
        {
            InitializeComponent();
        }

        private void medicineBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.medicineBindingSource.EndEdit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.databasesTableAdapter.Fill(this.masterDataSet.databases);
        }
        public static Color[] GetUniqueRandomColor(int count)
        {
            Color[] colors = new Color[count];
            HashSet<Color> hs = new HashSet<Color>();
            Random randomColor = new Random();
            for (int i = 0; i < count; i++)
            {
                Color color;
                while (!hs.Add(color = Color.FromArgb(randomColor.Next(70, 200), randomColor.Next(100, 225), randomColor.Next(100, 230)))) ;
                colors[i] = color;
            }
            return colors;
        }
        public void vekhung(Graphics g)
        {
            g.Clear(Color.White);
            Pen mypen = new Pen(Color.Black);
            Pen mypen1 = new Pen(Color.LightCyan);
            int x = 600;
            int y = 535;
            for (int i = 0; i < x;)
            {
                g.DrawLine(mypen1, 0, i, x, i);
                i = i + 5;
            }
            for (int i = 0; i < x;)
            {
                g.DrawLine(mypen1, i, 0, i, y);
                i = i + 5;
            }
            g.DrawLine(mypen, 25, 20, 25, 505);
            g.DrawLine(mypen, 25, 505, 550, 505);
            g.DrawRectangle(mypen, 0, 0, 585, 530);
            Brush mybrush = new SolidBrush(Color.Black);
            Point p1 = new Point(25, 15);
            Point p2 = new Point(30, 25);
            Point p3 = new Point(20, 25);
            Point[] p = { p1, p2, p3 };
            g.DrawPolygon(mypen, p);
            g.FillPolygon(mybrush, p);
            Point p4 = new Point(560, 505);
            Point p5 = new Point(550, 500);
            Point p6 = new Point(550, 510);
            Point[] a = { p4, p5, p6 };
            g.DrawPolygon(mypen, a);
            g.FillPolygon(mybrush, a);
            System.Drawing.Font f = new System.Drawing.Font("Arial", 7);
            g.DrawString("0", f, mybrush, new Point(20, 505));
            if(colnames.Length>1 && !colnames[1].Equals(""))
                g.DrawString(colnames[1], f, mybrush, new Point(1,1));
            g.DrawString(colnames[0], f, mybrush, new Point(495,510));
        }
        private void button3_MouseClick(object sender, EventArgs e)
        {
            if (cblColumns.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn dữ liệu để tính toán thuật toán!");
            }
            else
            {
                String vitri="";
                for (int i = 0; i < arrayOfDoubles.Length - 1; ++i)
                    for (int j = i + 1; j < arrayOfDoubles.Length; ++j)
                        if (Enumerable.SequenceEqual(arrayOfDoubles[i], arrayOfDoubles[j]))
                            vitri += j + " ";
                var s = vitri.Split(' ');
                String[] q = s.Distinct().ToArray();
                int maxcluster = arrayOfDoubles.Length - q.Length + 1;
                Graphics g = phancum.CreateGraphics();
                vekhung(g);
                if (k.Text.Trim() == "")
                {
                    MessageBox.Show("Số cụm k không được bỏ trống !", "Thông báo !", MessageBoxButtons.OK);
                    k.Focus();
                }
                int kcluster = 0;
                if (int.TryParse(k.Text, out kcluster))
                    if (kcluster < 2)
                    {
                        MessageBox.Show("Giá trị k phải lớn hơn 1, vui lòng nhập lại!");
                        k.Text = "";
                    }
                    else if (kcluster > maxcluster)
                    {
                        MessageBox.Show("Giá trị k phải nhỏ hơn hoặc bằng " + maxcluster + ", vui lòng nhập lại!");
                        k.Text = "";
                    }
                    else if (arrayOfDoubles[0].Length > 1)
                    {
                        //Color[] pointColor = GetUniqueRandomColor(kcluster);
                        Color[] pointColor = new Color[] { Color.Red, Color.LightBlue, Color.LawnGreen, Color.Orange, Color.Black, Color.Cyan, Color.DeepSkyBlue, Color.ForestGreen, Color.HotPink, Color.Yellow};
                        string[] pointColorName = new string[] { "màu đỏ", "màu xanh dương", "màu xanh lá cây", "màu cam", "màu đen", "màu xanh dạ quang", "màu xanh đen", "màu xanh lá đậm", "màu hồng đậm", "màu vàng"};
                        int[] clustering = Program.Cluster(arrayOfDoubles, kcluster);
                        
                        String r = "";
                        rs = new String[arrayOfDoubles.Length, 2];
                        for (int i = 0; i < arrayOfDoubles.Length; ++i)//dòng
                        {
                            for (int j = 0; j < 2; j++)//cột
                            {
                                if (j % 2 == 0)
                                {
                                    rs[i, j] = clustering[i] + "";
                                }
                                else
                                {
                                    rs[i, j] = pointname[i];
                                }
                            }
                        }
                        String re = "";
                        meanResults = new string[kcluster];
                        double[][] avr = new double[kcluster][];
                        for (int k = 0; k < kcluster; ++k)
                            avr[k] = new double[arrayOfDoubles.Length];
                        for (int k = 0; k < kcluster; k++)
                        {
                            re = "";
                            for (int j = 0; j < arrayOfDoubles[0].Length; ++j)
                            {
                                int count = 0;
                                double colSum = 0.0;
                                for (int i = 0; i < arrayOfDoubles.Length; ++i)
                                {
                                    if (k == clustering[i])
                                    {
                                        count++;
                                        colSum += arrayOfDoubles[i][j];
                                    }
                                }
                                avr[k][j] = Math.Round(colSum / count, 2);
                                re += "giá trị trung bình của [" + colnames[j] + "] là " + avr[k][j]+ "\r\n";
                            }
                            meanResults[k] = re;
                        }
                        int ic = 0;
                        while (ic < kcluster)
                        {
                            r += "CỤM " + (ic + 1) + "(" + pointColorName[ic] + "): \r\nVới "+ meanResults[ic] +"Các mẫu dữ liệu thuộc cụm: ";
                            for (int i = 0; i < rs.GetLength(0); i++)//dòng
                            {
                                for (int j = 0; j < rs.GetLength(1);)//cột
                                {
                                    if (rs[i, j].Equals(ic + ""))
                                    {
                                        j++; r += rs[i, j];
                                        r += ", ";
                                    }
                                    else { j += 2; }
                                }
                            }
                            r = r.Remove(r.Length - 2);
                            r += "\r\n\r\n";
                            ic++;
                        }
                        results.Text = r;
                        int size;
                        if (int.TryParse(sz.Text, out size))
                            if (size < 1)
                            {
                                MessageBox.Show("Kích thước biểu đồ phải lớn hơn 1, vui lòng nhập lại!");
                                k.Text = "";
                            }
                        for (int i = 0; i < point.GetLength(0); i++)
                        {
                            for (int j = 0; j < point.GetLength(1); j++)
                            {//nếu rất nhiều float => hiển thị theo công thức, chỉ sửa số hiện lên nha!
                                int x_axis = System.Convert.ToInt32(System.Math.Floor(point[i, j])) * size;
                                j++;
                                int y_axis = System.Convert.ToInt32(System.Math.Floor(point[i, j])) * size;
                                int x1 = 25 + 5 * (x_axis);
                                int y1 = 505 - 5 * (y_axis);
                                Pen blackPen = new Pen(Color.Black);
                                Brush myb = new SolidBrush(pointColor[clustering[i]]);
                                Rectangle rect = new Rectangle(x1 - 3, y1 - 3, 5, 5);
                                float startAngle = 0.0F;
                                float sweepAngle = 360.0F;
                                g.DrawPie(blackPen, rect, startAngle, sweepAngle);
                                g.FillPie(myb, rect, startAngle, sweepAngle);
                                System.Drawing.Font font = new System.Drawing.Font("Arial", (int)(size * 1.43));
                                //g.DrawString("(" + x_axis / size + "," + y_axis / size + ")", font, myb, new Point(x1 + 5, y1 + 5));
                                g.DrawString(pointname[i], font, myb, new Point(x1 + 5, y1 - 12));
                            }
                        }
                    }
                    else if (arrayOfDoubles[0].Length <= 1)
                    {
                        MessageBox.Show("Để thực thi thuật toán, vui lòng chọn nhiều hơn 1 cột dữ liệu đối với những cột dữ liệu là số", "THÔNG BÁO");
                    }
            }
        }

        private void cblColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cot = "";
            if (cblColumns.CheckedItems.Count != 0)
            {
                List<string> selected = new List<string>(); //selected: Tên các cột đã chọn
                for (int x = 0; x < cblColumns.CheckedItems.Count; x++)
                {
                    selected.Add(cblColumns.CheckedItems[x].ToString());
                }
                for (int i = 0; i < selected.Count; i++)
                {
                    if (i == (selected.Count - 1))
                    {
                        cot += "[" + selected[i] + "]";
                    }
                    else
                    {
                        cot += "[" + selected[i] + "]" + ", ";
                    }
                }
            }
            if (!cot.Equals(""))
            {
                String dulieubang = "USE " + SYS_DB + " SELECT " + cot + " FROM " + SYS_TABLE;
                if (Program.KetNoi() == 0) return;
                Program.myReader = Program.ExecSqlDataReader(dulieubang);
                var datatable = new DataTable();
                datatable.Load(Program.myReader);
                String cottendiem = "";
                Boolean flag = false;

                //VÒNG LẶP NÀY CÓ NHIỆM VỤ XÁC ĐỊNH CHO BIỂU ĐỒ CÓ TÊN HAY KHÔNG VÀ LOẠI BỎ CỘT DỮ LIỆU KHÔNG THUỘC KIỂU MONG MUỐN
                var delete = new List<String>();
                for (int i = 0; i < datatable.Columns.Count; i++)
                {
                    String temp = datatable.Columns[i].DataType.ToString();
                    if (temp.Equals("System.String"))
                    {
                        flag = true;
                        if (cottendiem.Equals(""))
                        {
                            cottendiem = datatable.Columns[i].ColumnName;
                        }
                        delete.Add(datatable.Columns[i].ColumnName);
                    }
                    else if (!(temp.Equals("System.Double") || temp.Equals("System.Boolean") || temp.Equals("System.Byte") || temp.Equals("System.Int16") || temp.Equals("System.Int32") || temp.Equals("System.Int64") || temp.Equals("System.Single") || temp.Equals("System.Decimal")))
                    {
                        delete.Add(datatable.Columns[i].ColumnName);
                    }
                }
                for (int i = 0; i < delete.Count; i++)
                {
                    datatable.Columns.Remove(delete[i]);
                }
                if (flag == true)
                {
                    String pname = "USE " + SYS_DB + " SELECT " + cottendiem + " FROM " + SYS_TABLE;
                    if (Program.KetNoi() == 0) return;
                    Program.myReader = Program.ExecSqlDataReader(pname);
                    var datatablename = new DataTable();
                    datatablename.Load(Program.myReader);
                    pointname = datatablename.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
                }
                else
                {
                    const String conststr = "Point ";
                    pointname = new String[datatable.Rows.Count];
                    for (int i = 1; i <= datatable.Rows.Count; i++)
                    {
                        pointname.SetValue(conststr + i, i - 1);
                    }
                }

                //HIỂN THỊ CỘT DỮ LIỆU TƯƠNG ỨNG KHI CHỌN CỘT
                string constring = "Data Source=DESKTOP-UPGKA2J;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=123";
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                SqlCommand cmd = new SqlCommand(dulieubang, con);
                SqlDataReader r = cmd.ExecuteReader();
                DataTable tbl = new DataTable();
                tbl.Load(r);
                dataGridView1.DataSource = tbl;
                //DỮ LIỆU TÍNH TOÁN K-MEANS ÉP QUA KIỂU DOUBLE TỪ DỮ LIỆU DATATABLE
                int number_cols = datatable.Columns.Count;
                int number_rows = datatable.Rows.Count;
                arrayOfDoubles = new double[number_rows][];
                colnames = new string[number_cols];
                for (int i = 0; i < number_rows; i++)
                {
                    double[] d = new double[number_cols];
                    for (int j = 0; j < number_cols; j++)
                    {
                        Type t = (datatable.Rows[i].ItemArray[0]).GetType();
                        String temp = t + "";
                        if (!temp.Equals("System.String"))
                        {
                            d[j] = Convert.ToDouble(datatable.Rows[i].ItemArray[j]);
                            colnames[j] = datatable.Columns[j].ColumnName;
                        }
                        else
                        {
                            break;
                        }
                    }
                    arrayOfDoubles[i] = d;
                }

                //DỮ LIỆU ĐIỂM HIỂN THỊ TRÊN BIỂU ĐỒ
                String chon = "";
                point = new double[arrayOfDoubles.Length, 2];
                for (int i = 0; i < arrayOfDoubles.Length; ++i)//dòng
                {
                    for (int j = 0; j < arrayOfDoubles[i].Length; ++j)//cột
                    {
                        if (j < 2)
                        {
                            point[i, j] = arrayOfDoubles[i][j];
                            chon += arrayOfDoubles[i][j] + ". ";
                        }
                    }
                }
            }
        }
        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            bang.Items.Clear();
            SYS_DB = "";
            int numrow = e.RowHandle;
            String db = gridView2.GetRowCellValue(numrow, "name").ToString();
            SYS_DB = db;
            //String sql = "SELECT TABLE_NAME FROM " + db + ".INFORMATION_SCHEMA.TABLES";
            String sql = "USE " + SYS_DB + " SELECT NAME FROM SYS.sysobjects WHERE XTYPE = 'V' OR XTYPE = 'U'";
            if (Program.KetNoi() == 0) return;
            List<string> tables = new List<string>();
            SqlDataReader tableReader = Program.ExecSqlDataReader(sql);
            while (tableReader.Read())
            {
                tables.Add(tableReader.GetString(0).Trim());
            }
            for (int i = 0; i < tables.Count; i++)
            {
                bang.Items.Add(tables[i]);
            }
        }

        private void bang_SelectedIndexChanged(object sender, EventArgs e)
        {
            SYS_TABLE = "";
            dt = new DataTable();
            int count = bang.Items.Count;
            int index = bang.SelectedIndex;
            if (count == 1)
            {
                cblColumns.Items.Clear();
                SYS_TABLE = bang.Items[index].ToString();
                if (Program.KetNoi() == 0) return;
                List<string> columns = new List<string>();
                String tencot = "use " + SYS_DB + " select name from sys.columns where object_id = OBJECT_ID('" + bang.SelectedItem.ToString() + "') ";
                SqlDataReader columnsReader = Program.ExecSqlDataReader(tencot);
                while (columnsReader.Read())
                {
                    columns.Add(columnsReader.GetString(0).Trim());
                }
                for (int j = 0; j < columns.Count; j++)
                {
                    cblColumns.Items.Add(columns[j]);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (index != i)
                    {
                        SYS_TABLE = bang.Items[index].ToString();
                        cblColumns.Items.Clear();
                        if (count > 1)
                        {
                            bang.SetItemCheckState(i, CheckState.Unchecked);
                        }
                        if (Program.KetNoi() == 0) return;
                        List<string> columns = new List<string>();
                        String tencot = "use " + SYS_DB + " select name from sys.columns where object_id = OBJECT_ID('" + bang.SelectedItem.ToString() + "') ";
                        SqlDataReader columnsReader = Program.ExecSqlDataReader(tencot);
                        while (columnsReader.Read())
                        {
                            columns.Add(columnsReader.GetString(0).Trim());
                        }
                        for (int j = 0; j < columns.Count; j++)
                        {
                            cblColumns.Items.Add(columns[j]);
                        }
                    }
                }
            }
        }
    }
}
