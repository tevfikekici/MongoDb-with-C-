using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MongoDbTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");
           
            // INSERT ------------------------------------------
            UserModel user = new UserModel
            {
                Name = "Johan",
                Surname = "Londoner",
                Password = "XDESA",
                PrimaryAddress = new AddressModel
                {

                    StreetAddress = "Test address",
                    City = "Krakow",
                    State = "Malopolskie",
                    ZipCode = "05-123"

                }
            };


            // db.InsertRecord("Users", new UserModel { Name = "TestName", Surname = "TestSurname", Password = "123" });
            // db.InsertRecord("Users", user);
            // INSERT ------------------------------------------

            // GET ALL ------------------------------------------
            var recs = db.LoadRecords<UserModel>("Users");

            foreach (var rec in recs)
            {
                if (rec.PrimaryAddress != null)
                {

                    listBox1.Items.Add(rec.id.ToString() + " - " + rec.Name + " - " + rec.Surname + " - " + rec.PrimaryAddress.City);
                }
                else
                {
                    listBox1.Items.Add(rec.id.ToString() + " - " + rec.Name + " - " + rec.Surname);

                }
            }
            // GET ALL ------------------------------------------

            // GET ONE ------------------------------------------

            var record = db.LoadById<UserModel>("Users", ObjectId.Parse("60dd8dd819538db81d6d429e"));
            if (record.PrimaryAddress != null)
            {

                listBox2.Items.Add(record.id.ToString() + " - " + record.Name + " - " + record.Surname + " - " + record.PrimaryAddress.City);
            }
            else
            {
                listBox2.Items.Add(record.id.ToString() + " - " + record.Name + " - " + record.Surname);

            }

            // GET ONE ------------------------------------------

            //  UPDATE ------------------------------------------
            record.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            db.UpsertRecord("Users", record.id, record);

            // DELETE ------------------------------------------
            //db.DeleteRecord<UserModel>("Users", record.id);

           
        }
    }
}
