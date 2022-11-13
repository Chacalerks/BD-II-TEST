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
    public partial class adminFormSucursal : System.Web.UI.Page
    {
        private string stringConnection = ConfigurationManager.ConnectionStrings["connectionFernanda"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonAgregarSucursal_Click(object sender, EventArgs e)
        {

            string idSucursal = TextBoxIDSucursal.Text;
            string nombreSucursal = TextBoxNombreSucursal.Text;
            string idLugar = TextBoxIDLugar.Text;
            string idMonedaXPais = TextBoxIdMonedaXP.Text;
            string estado = DropDownListEstado.SelectedValue;
            int valueResult = 0;
            string msgResult = "";

            if (nombreSucursal != "" && idLugar != "" && idMonedaXPais != "")
            {
                try
                {
                    SqlConnection connectionFernanda = new SqlConnection(stringConnection);
                    if (connectionFernanda.State == ConnectionState.Closed)
                    {
                        connectionFernanda.Open();


                    }
                    SqlCommand cmd = new SqlCommand("crudSucursal", connectionFernanda);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@opcion", 1);
                    cmd.Parameters.AddWithValue("@nombre", SqlDbType.Int).Value = nombreSucursal;
                    cmd.Parameters.AddWithValue("@idLugar", SqlDbType.Int).Value = Convert.ToInt32(idLugar);
                    cmd.Parameters.AddWithValue("@idMonedaxPais", SqlDbType.Int).Value = Convert.ToInt32(idMonedaXPais);


                    SqlDataReader reader = cmd.ExecuteReader();
       

                    connectionFernanda.Close();
                    reader.Close();

                    if (valueResult == 0)
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                            "Swal.fire('Perfect','" + msgResult + "','s')", true);
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                            "Swal.fire('Error','" + msgResult + "','error')", true);
                    }


                    //Response.Write(alert);

                }
                catch (Exception ex)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                           "Swal.fire('Error','" + ex.Message + "','error')", true);
                }
                /// Titulo, mensaje, tipo
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                           "Swal.fire('Error','Some files are empty','error')", true);
            }

        
        }

        protected void ButtonActualizarSucursal_Click(object sender, EventArgs e)
        {

        }
    }
}