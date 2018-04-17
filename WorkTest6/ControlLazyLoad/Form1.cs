using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ControlLazyLoad
{
    public partial class Form1 : Form
    {
        private List<Person> lstData = new List<Person>();
        protected int PageCount { get; set; } = 1;
        protected int PageSize { get; } = 10;
        private List<Person> lstBindData = new List<Person>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 1000; i++)
            {
                lstData.Add(new Person() { Id = i, Name = ((char)i).ToString(), Age = i });
            }
            lstBindData = lstData.Take(20).ToList();


            searchLookUpEdit1.BindData("Name","Id",1,20,(string filterText, int pageIndex, int pageSize, out int count)  => {
                if (string.IsNullOrEmpty(filterText))
                {
                    count = lstData.Count;
                    return lstData.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    var result = lstData.Where(p => p.Name.Contains(filterText) || p.Age.ToString().Contains(filterText));
                    count = result.Count();
                    return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }

            });

            lookUpEdit1.Properties.DataSource = lstBindData;
            lookUpEdit1.Properties.DisplayMember = "Name";
            lookUpEdit1.Properties.ValueMember = "Id";

            lookUpEdit1.ProcessNewValue += LookUpEdit1_ProcessNewValue;

            

        }

        private void LookUpEdit1_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            Console.WriteLine(e.DisplayValue);
        }
    }
}
