using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

[DataObject]
public class ProductDB
{
    [DataObjectMethod(DataObjectMethodType.Select)]
    public static List<Product> GetProducts()
    {
        List<Product> productList = new List<Product>();
        string sql = "SELECT ProductID, Name, OnHand FROM Products";

        using (SqlConnection con = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                Product product;
                while (rdr.Read())
                {
                    product = new Product();
                    product.ProductID = rdr["ProductID"].ToString();
                    product.Name = rdr["Name"].ToString();
                    product.OnHand = Convert.ToInt32(rdr["Onhand"]);
                    productList.Add(product);
                }
                rdr.Close();
                return productList;

            } // dispose of command object
        } // close connection and dispose of object     
    }

    [DataObjectMethod(DataObjectMethodType.Update)]
    public static void UpdateProduct(Product product)
    {
        string sql =
            "UPDATE Products " +
            "SET OnHand = @OnHand " +
            "WHERE ProductID = @original_ProductID";

        using (SqlConnection con = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@OnHand", product.OnHand);
                cmd.Parameters.AddWithValue("@original_ProductID", product.ProductID);
                con.Open();
                cmd.ExecuteNonQuery();

            } // dispose of command object
        } // close connection and dispose of object     
    }

    private static string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings[
            "HalloweenConnection"].ConnectionString;
    }
}