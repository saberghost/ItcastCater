using CaterBll;
using CaterModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPOIDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ManagerInfoBll miBll = new ManagerInfoBll();
            var list = miBll.GetList();
            dataGridView1.DataSource = list;
            //创建Excel工作薄
            XSSFWorkbook workbook = new XSSFWorkbook();
            //创建单元格样式
            ICellStyle cellTitleStyle=workbook.CreateCellStyle();
            //设置单元格居中显示
            cellTitleStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //创建字体
            IFont font=workbook.CreateFont();
            //设置字体加粗显示
            font.IsBold = true;
            cellTitleStyle.SetFont(font);
            //创建Excel工作表
            ISheet sheet = workbook.CreateSheet("管理员");
            //创建Excel行
            IRow row = sheet.CreateRow(0);
            //创建Excel单元格
            NPOI.SS.UserModel.ICell cell = row.CreateCell(0);
            //设置单元格值
            cell.SetCellValue("管理员管理");
            //设置单元格合并
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 3));
            cell.CellStyle = cellTitleStyle;
            for (int i = 0; i < list.Count; i++)
            {
                IRow rowDate = sheet.CreateRow(i + 1);
                Type t = list[i].GetType();
                int count = 0;
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    object value = pi.GetValue(list[i]);
                    string name = pi.Name;
                    NPOI.SS.UserModel.ICell cellDate = rowDate.CreateCell(count);
                    if (i == 0)
                    {
                        cellDate.SetCellValue(name);
                    }else
                    {
                        cellDate.SetCellValue(value.ToString());
                    }
                    sheet.AutoSizeColumn(count);
                    count++;
                }
            }
            using (FileStream fs = new FileStream(@"C:\Users\Saber\Desktop\Demo.xlsx", FileMode.OpenOrCreate))
            {
                workbook.Write(fs);
            }

        }

        private void btnExcelImport_Click(object sender, EventArgs e)
        {
            List<ManagerInfo> list = new List<ManagerInfo>();
            using (FileStream fs = new FileStream(@"C: \Users\Saber\Desktop\Demo.xlsx", FileMode.Open))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheetAt(0);
                int rowIndex = 1;
                while (sheet.GetRow(++rowIndex) != null)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    ManagerInfo mi = new ManagerInfo()
                    {
                        Mid = Convert.ToInt32(row.GetCell(0).StringCellValue),
                        Mname = row.GetCell(1).StringCellValue,
                        Mpwd = row.GetCell(2).StringCellValue,
                        Mtype = Convert.ToInt32(row.GetCell(3).StringCellValue)
                    };
                    list.Add(mi);
                }
                dataGridView1.DataSource = list;
            }
        }

        private void btnWordExport_Click(object sender, EventArgs e)
        {
            XWPFDocument doc = new XWPFDocument();
            XWPFParagraph p0 = doc.CreateParagraph();
            p0.Alignment = ParagraphAlignment.CENTER;
            XWPFRun r0= p0.CreateRun();
            r0.SetUnderline(UnderlinePatterns.Dash);
            r0.SetFontFamily("方正准圆_GBK", FontCharRange.None);
            r0.SetText("我们");
            using(FileStream fs=new FileStream(@"C: \Users\Saber\Desktop\Demo.docx", FileMode.OpenOrCreate))
            {
                doc.Write(fs);
            }
        }
    }
}
