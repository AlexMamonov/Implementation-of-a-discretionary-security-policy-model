using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Org.BouncyCastle.Crypto.Digests;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Объявим структуру хранения прав доступа (access rights)
        struct accessRights
        {
            public right fileServer;
            public right databaseServer;
            public right documentServer;
            public right webServer;
        }
        public enum right
        {
            None,
            Guest,
            User,
            Moderator,
            Admininstrator
        }

        struct user
        {
            public string name;
            public accessRights rights;
        }
        int usersCount;
        
        user[] listOfUsers;
        Random r = new Random();

        private void LbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Очищаем компонент dataGridView
            dgwUsersRights.Rows.Clear();
            // Выводим персональные данные выбранного пользователя
            dgwUsersRights.Rows.Add("Файловый сервер",listOfUsers[lbUsers.SelectedIndex].rights.fileServer.ToString());
            dgwUsersRights.Rows.Add("Сервер баз данных",listOfUsers[lbUsers.SelectedIndex].rights.databaseServer.ToString());
            dgwUsersRights.Rows.Add("Сервер документов",listOfUsers[lbUsers.SelectedIndex].rights.documentServer.ToString());
            dgwUsersRights.Rows.Add("Web-сервер",listOfUsers[lbUsers.SelectedIndex].rights.webServer.ToString());
            dgwUsersRights.Rows.Add("Пароль", listOfUsers[lbUsers.SelectedIndex].rights.documentServer.ToString());
            dgwUsersRights.Rows.Add("Пароль Пк", listOfUsers[lbUsers.SelectedIndex].rights.documentServer.ToString());
            dgwUsersRights.Rows.Add("Пароль Вин", listOfUsers[lbUsers.SelectedIndex].rights.documentServer.ToString());
            if (lbUsers.SelectedIndex == usersCount)
            {
                listBox1.Items.Clear();
                for (int i = 0; i < usersCount; i++)
                {
                    listBox1.Items.Add(listOfUsers[i].name);
                }
            }
            else
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();
            }
        }

        private void DgwUsersRights_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TbUserCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Получаем количество пользователей
            usersCount = Convert.ToInt32(tbUserCount.Text);
            // Очищаем ListBox
            lbUsers.Items.Clear();
            // Выделяем памать под статический массив пользователей
            listOfUsers = new user[usersCount + 1]; // +1 т.к. Администратор отдельно
                                                    // В цикле заполняем массив пользователей
            for (int i = 0; i < usersCount; i++)
            {
                listOfUsers[i].name = "Пользователь №" + (i + 1).ToString();
                listOfUsers[i].rights.fileServer = (right)r.Next(0, 5);
                listOfUsers[i].rights.databaseServer = (right)r.Next(0, 5);
                listOfUsers[i].rights.documentServer = (right)r.Next(0, 5);
                listOfUsers[i].rights.webServer = (right)r.Next(0, 5);
              
            }
            // Отдельно добавляем администратора
            listOfUsers[usersCount].name = "Администратор";
            listOfUsers[usersCount].rights.fileServer = right.Admininstrator;
            listOfUsers[usersCount].rights.databaseServer = right.Admininstrator;
            listOfUsers[usersCount].rights.documentServer = right.Admininstrator;
            listOfUsers[usersCount].rights.webServer = right.Admininstrator;
            




            // В цикле выводим список пользователей
            for (int i = 0; i < usersCount + 1; i++)
            {
                lbUsers.Items.Add(listOfUsers[i].name);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < usersCount; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (listBox2.Items.IndexOf("Фаловый сервер") == listBox2.SelectedIndex && listBox1.Items.IndexOf(listOfUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox3.SelectedIndex == i)
                        {
                            listOfUsers[j].rights.fileServer = (right)i;
                        }
                    }
                    if (listBox2.Items.IndexOf("Сервер баз данных") == listBox2.SelectedIndex && listBox1.Items.IndexOf(listOfUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox3.SelectedIndex == i)
                        {
                            listOfUsers[j].rights.databaseServer = (right)i;
                        }
                    }
                    if (listBox2.Items.IndexOf("Сервер документов") == listBox2.SelectedIndex && listBox1.Items.IndexOf(listOfUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox3.SelectedIndex == i)
                        {
                            listOfUsers[j].rights.documentServer = (right)i;
                        }
                    }
                    if (listBox2.Items.IndexOf("Web - сервер") == listBox2.SelectedIndex && listBox1.Items.IndexOf(listOfUsers[j].name) == listBox1.SelectedIndex)
                    {
                        if (listBox3.SelectedIndex == i)
                        {
                            listOfUsers[j].rights.webServer = (right)i;
                        }
                    }
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                listBox3.Items.Add((right)i);
            }
        }

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("Фаловый сервер");
            listBox2.Items.Add("Сервер баз данных");
            listBox2.Items.Add("Сервер документов");
            listBox2.Items.Add("Web - сервер");
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            lbUsers.Items.Clear();
            OpenFileDialog openPolicyDialog = new OpenFileDialog();
            // Добавляем фильтр к компоненту openFileDialog, чтобы он позволял выбрать только файлы типа xml
            openPolicyDialog.Filter = "XML файлы(*.xml)|*.xml";
            // Запускаем openFileDialog с использованием функции ShowDialog
            if (openPolicyDialog.ShowDialog() == DialogResult.Cancel)
                return; // отменяем действие при нажатии кнопки отмены
                        // Получаем выбранный файл
            string filename = openPolicyDialog.FileName;


            // Открываем XML документ
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            List<UserInPolicy> listOFUsersClass = new List<UserInPolicy>();
            // Цикл для каждого элемента из документа

            int n = 0;
            foreach (XmlNode node in doc.DocumentElement)
            {
                // Считываем данные из файла
                string name = node.Attributes["name"].Value;
                int rightFileServer = int.Parse(node["fileServer"].InnerText);
                int rightDatabaseServer = int.Parse(node["databaseServer"].InnerText);
                int rightDocumentServer = int.Parse(node["documentServer"].InnerText);
                int rightWebServer = int.Parse(node["webServer"].InnerText);
                UserInPolicy currentUser = new UserInPolicy(name, rightFileServer,
               rightDatabaseServer, rightDocumentServer, rightWebServer);

                listOFUsersClass.Add(currentUser);
                n++;
            }
            listOfUsers = new user[n];
            for (int i=0; i<n; i++ )
            {
                UserInPolicy item = listOFUsersClass[i];
                listOfUsers[i].name = item.Name;
                listOfUsers[i].rights.fileServer = (right)item.RightFileServer;
                listOfUsers[i].rights.databaseServer = (right)item.RightDatabaseServer;
                listOfUsers[i].rights.documentServer = (right)item.RightDocumentServer;
                listOfUsers[i].rights.webServer = (right)item.RightWebServer;
                lbUsers.Items.Add(listOfUsers[i].name);
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            SaveFileDialog savePolicyFileDialog = new SaveFileDialog();
            /*
 * Сохраняем сформированную политику безопасности в виде XML-файла
 */
            // Создаём новый XMLдокумент
            XmlDocument xDoc = new XmlDocument();
            // Создаем Xml заголовок
            var xmlDeclaration = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            // Добавляем заголовок перед корневым элементом.
            xDoc.AppendChild(xmlDeclaration);
            XmlElement xRoot = xDoc.CreateElement("securitypolicy");

            // Цикл по записям
            for (int i = 0; i < usersCount+1; i++)
            {
                // Создадим новый элемент, характеризующий пользователя
                XmlElement userElem = xDoc.CreateElement("user");
                // Создадим атрибут name
                XmlAttribute nameAttr = xDoc.CreateAttribute("name");
                // Создадим элементы для хранения прав доступа
                XmlElement fileServerElem = xDoc.CreateElement("fileServer");
                XmlElement databaseServerElem = xDoc.CreateElement("databaseServer");
                XmlElement documentServerElem = xDoc.CreateElement("documentServer");
                XmlElement webServerElem = xDoc.CreateElement("webServer");
                // Зададим текстовые значения для элементов и атрибута
                XmlText nameText = xDoc.CreateTextNode(listOfUsers[i].name);
                XmlText fileServerText = xDoc.CreateTextNode(((int)listOfUsers[i].rights.fileServer).ToString());
                XmlText databaseServerText = xDoc.CreateTextNode(((int)listOfUsers[i].rights.databaseServer).ToString());
                XmlText documentServerText = xDoc.CreateTextNode(((int)listOfUsers[i].rights.documentServer).ToString());
                XmlText webServerText = xDoc.CreateTextNode(((int)listOfUsers[i].rights.webServer).ToString());
                // Теперь добавим узлы
                nameAttr.AppendChild(nameText);
                fileServerElem.AppendChild(fileServerText);
                databaseServerElem.AppendChild(databaseServerText);
                documentServerElem.AppendChild(documentServerText);
                webServerElem.AppendChild(webServerText);
                userElem.Attributes.Append(nameAttr);
                userElem.AppendChild(fileServerElem);
                userElem.AppendChild(databaseServerElem);
                userElem.AppendChild(documentServerElem);
                userElem.AppendChild(webServerElem);
                // Добавляем новый корневой элемент в документ.
                xRoot.AppendChild(userElem);
                xDoc.AppendChild(xRoot);
            }

            savePolicyFileDialog.Filter = "XML файлы(*.xml)|*.xml";
            // Вызыавем диалог сохранения файла
            if (savePolicyFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            // получаем выбранный файл
            string filename = savePolicyFileDialog.FileName;

            xDoc.Save(filename);
        }
    }

}

