using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AplicacionFutbol.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AplicacionFutbol.Controllers
{
    public class JugadorDAO
    {
        string cad_cn = ConfigurationManager.ConnectionStrings["cn1"].ConnectionString;


        public List<Continente> ListarContinentes()
        {
            List<Continente> lista = new List<Continente>();
            SqlConnection cn = new SqlConnection(cad_cn);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAR_CONTINENTE", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Continente con = new Continente()
                {
                    ide_con = int.Parse(lector[0].ToString()),
                    nom_con = lector[1].ToString()
                };
                lista.Add(con);
            }

            cn.Close();
            return lista;
        }

        public List<Pais> ListarPaises(int ide_con_pais)
        {
            List<Pais> lista = new List<Pais>();
            SqlConnection cn = new SqlConnection(cad_cn);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAR_PAIS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IDE_CON_PAIS", ide_con_pais);
            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Pais pai = new Pais()
                {
                    ide_pais = int.Parse(lector[0].ToString()),
                    nom_pais = lector[1].ToString(),
                    ide_con_pais = int.Parse(lector[2].ToString())
                };

                lista.Add(pai);
            }

            cn.Close();
            return lista;
        }

        public List<Jugador> ListarJugador()
        {
            List<Jugador> lista = new List<Jugador>();
            SqlConnection cn = new SqlConnection(cad_cn);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTAR_JUGADOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader lector = cmd.ExecuteReader();

            while (lector.Read())
            {
                Jugador jug = new Jugador()
                {
                    ide_jug = int.Parse(lector[0].ToString()),
                    nom_jug = lector[1].ToString(),
                    fna_jug = DateTime.Parse(lector[2].ToString()),
                    ide_pais = int.Parse(lector[3].ToString()),
                    nom_pais = lector[4].ToString(),
                    ide_con = int.Parse(lector[5].ToString()),
                    nom_con = lector[6].ToString(),
                    pes_jug = Decimal.Parse(lector[7].ToString()),
                };

                lista.Add(jug);
            }

            cn.Close();
            return lista;
        }



        public void InsertarJugador(Jugador objJug)
        {
            try
            {
                SqlConnection cn = new SqlConnection(cad_cn);
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_INSERTAR_JUGADOR", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NOM_JUG", objJug.nom_jug);
                cmd.Parameters.AddWithValue("@FNA_JUG", objJug.fna_jug);
                cmd.Parameters.AddWithValue("@IDE_PAIS", objJug.nom_pais);
                cmd.Parameters.AddWithValue("@IDE_CON", objJug.nom_con);
                cmd.Parameters.AddWithValue("@PES_JUG", objJug.pes_jug);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarJugador(Jugador objJug)
        {
            try
            {
                SqlConnection cn = new SqlConnection(cad_cn);
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_ACTUALIZAR_JUGADOR", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE_JUG", objJug.ide_jug);
                cmd.Parameters.AddWithValue("@NOM_JUG", objJug.nom_jug);
                cmd.Parameters.AddWithValue("@FNA_JUG", objJug.fna_jug);
                cmd.Parameters.AddWithValue("@IDE_PAIS", objJug.ide_pais);
                cmd.Parameters.AddWithValue("@IDE_CON", objJug.ide_con);
                cmd.Parameters.AddWithValue("@PES_JUG", objJug.pes_jug);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarJugador(int ide_jug)
        {
            try
            {
                SqlConnection cn = new SqlConnection(cad_cn);
                cn.Open();

                SqlCommand cmd = new SqlCommand("SP_ELIMINAR_JUGADOR", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IDE_JUG", ide_jug);
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}