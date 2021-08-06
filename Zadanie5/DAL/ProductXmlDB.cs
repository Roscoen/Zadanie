using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Xml;
using Zadanie5.Models;
using Zadanie5.DAL;
using Zadanie5.Pages;
using System.Xml.Linq;

namespace Zadanie5.DAL
{
    public class ProductXmlDB : IProductDB
    {
        XmlDocument db = new XmlDocument();
        string xmlDB_path;
        public ProductXmlDB(IConfiguration _configuration)
        {
            xmlDB_path = _configuration.GetValue<string>("AppSettings:XmlDB_path");

            LoadDB();
        }
        private void LoadDB()
        {
            db.Load(xmlDB_path);
        }
        public List<Product> List()
        {
            List<Product> productList = new List<Product>();
            XmlNodeList productXmlNodeList = db.SelectNodes("/store/product");

            foreach (XmlNode productXmlNode in productXmlNodeList)
            {
                productList.Add(XmlNodeProduct2Product(productXmlNode));
            }
            return productList;
        }
        private Product XmlNodeProduct2Product(XmlNode node)
        {
            Product p = new Product();
            p.id = int.Parse(node.Attributes["id"].Value);
            p.name = node["name"].InnerText;
            p.price = decimal.Parse(node["price"].InnerText);
            return p;
        }


        public Product Get(int _id) 
        {
            Product p = new Product();
            OpenXmlBase();
            XmlNode node = XmlNodeProductGet(_id);
            return XmlNodeProduct2Product(node);
        }
        private void OpenXmlBase()
        {
            db.Load("DATA/store.xml");
        }
        private void SaveXmlBase()
        {
            db.Save("DATA/store.xml");
        }
        private XmlNode XmlNodeProductGet(int _id)
        {
            XmlNode node = null;
            XmlNodeList list = db.SelectNodes("/store/product[@id=" + _id.ToString() + "]");
            node = list[0];
            return node;
        }


        public void Update(Product _product) 
        {
            OpenXmlBase();
            XmlNode node = XmlNodeProductGet(_product.id);
            node["name"].InnerText = _product.name;
            node["price"].InnerText = _product.price.ToString();
            SaveXmlBase();
        }

        private int GetNextId()
        {
            List<Product> productListId;
            productListId = List();
            int lastID = productListId[productListId.Count - 1].id;
            int newID = lastID + 1;
            return newID;
        }

        public void Delete(int _id) 
        {
            OpenXmlBase();
            //LoadDB();

            // find a node
            XmlNode node = db.SelectSingleNode("/store/product[@id=" + _id.ToString() + "]");

            // if found....
            if (node != null)
            {
                // get its parent node
                XmlNode parent = node.ParentNode;

                // remove the child node
                parent.RemoveChild(node);

                // verify the new XML structure
                string newXML = db.OuterXml;

                SaveXmlBase();
            }
        }

        public void Add(Product _product) 
        {
            OpenXmlBase();
            //LoadDB();

            _product.id = GetNextId();

            XmlNode product = db.CreateElement("product");
            XmlAttribute att = db.CreateAttribute("id");
            att.InnerText = _product.id.ToString();
            product.Attributes.Append(att);

            XmlNode name = db.CreateElement("name");
            name.InnerText = _product.name;
            product.AppendChild(name);

            XmlNode price = db.CreateElement("price");
            price.InnerText = _product.price.ToString();
            product.AppendChild(price);

            db.DocumentElement.AppendChild(product);
            SaveXmlBase();
        }
    }
}
