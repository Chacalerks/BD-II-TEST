﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace indioSupermercado
{
    public partial class branchSelector : System.Web.UI.Page
    {
        public string idCustomer = "";
        public int idSucursal = 0;
        public string nombreSucursal = "";
        public string dia = "";
        public string horaInicial = "";
        public string horaFinal = "";
        string strcon = usefull.strCon;
        public string[] customerCoords = new string[2];
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("spGetIdCustomerFromUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombreUsuario", SqlDbType.VarChar).Value = Session["username"].ToString();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                idCustomer = reader[0].ToString();
                customerCoords[0] = reader[1].ToString();
                customerCoords[1] = reader[2].ToString(); 
                reader.Close();

                SqlCommand cmd2 = new SqlCommand("spClosestPoint", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@idCliente", SqlDbType.VarChar).Value = idCustomer;
                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                if (reader2.HasRows)
                {
                    idSucursal = Convert.ToInt32(reader2[0].ToString());
                    nombreSucursal = reader2[1].ToString();
                    dia = reader2[2].ToString();
                    horaInicial = reader2[3].ToString();
                    horaFinal = reader2[4].ToString();
                }
                reader2.Close();


                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "spGetOtherBranches "+idSucursal+"";
                cmd3.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd3);
                da.Fill(dt);
                d2.DataSource = dt;
                d2.DataBind();


                SqlCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "spGetBranchesLocation";
                cmd4.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd4);
                da2.Fill(dt2);
                addToMap(dt2); //agregar sucursales al mapa

                con.Close();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                            "Swal.fire('Error','" + ex.Message + "','error')", true);
            }
            
        }

        protected void addToMap(DataTable dt) {
            List<Tuple<float, float, string>> branchCoordinates = new List<Tuple<float, float, string>>();
            float x, y;
            string sx, sy, name;

            foreach (DataRow row in dt.Rows)
            {
                x = float.Parse(row["X"].ToString());
                y = float.Parse(row["Y"].ToString());
                name = row["nombreSucursal"].ToString();
                branchCoordinates.Add(Tuple.Create(x,y,name));
            }
            string strFunc = "addMarker([";
            int largo = branchCoordinates.Count;
            int cont = 1;
            foreach (var i in branchCoordinates)
            {
                sx = (i.Item1).ToString();
                sy = (i.Item2).ToString();
                name = i.Item3;
                if (cont < largo)
                {
                    strFunc = strFunc + "[" + sx + "," + sy + ",'" +name+"'],";
                }
                else {
                    strFunc = strFunc + "[" + sx + "," + sy + ",'" + name + "']";
                }
                cont++;
            }
            strFunc = strFunc + "],[" + customerCoords[0] + "," + customerCoords[1] +"])";

            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myFunc", strFunc, true);
        }

    }
}