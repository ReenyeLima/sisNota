<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="nota.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Nota fiscal eletronica</title>
    <link rel="stylesheet" href="styles/StyleSheet1.css" />
    <link href="https://fonts.googleapis.com/css?family=Abril+Fatface|Anton" rel="stylesheet"/>
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/JavaScript.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Josefin+Sans|PT+Sans+Narrow" rel="stylesheet"/>
</head>
<body>
    <form id="tudo" runat="server">

        <div id="nav">
            <ul>

                <li><span class="enviar"></span><a href="#" runat="server" onserverclick="Unnamed_ServerClick">Enviar RPS </a></li>
                <li><span class="info"></span><a href="#" runat="server" onserverclick="Unnamed_ServerClick1">Informações da empresa </a></li>
                <li><span class="consulta"></span><a href="#" runat="server" onserverclick="Unnamed_ServerClick2">Consulta</a></li>

            </ul>


        </div>
        <div id="view">
            <asp:MultiView ID="MultiView2" runat="server" OnActiveViewChanged="MultiView2_ActiveViewChanged">
                <asp:View ID="View1" runat="server">

                    <div id="enviararq">
                        <form id="upload">

                            <h1>Enviar arquivos</h1>
                            <img src="img/up2.png" /></><br />
                            <div class="upload-btn-wrapper">
                                <button class="btn">Escolha um arquivo</button>
                                <input type="file" name="myfile" /><br />
                            </div>

                            <center>  <asp:Button  ID="uploadbutton" runat="server" CssClass="btClass" Text="&#x21EA; Enviar" /> </center>

                        </form>
                    </div>
                </asp:View>


                <asp:View ID="View2" runat="server">
                    <div id="info">

                        <h2>Informações da empresa
                            <img src="img/infoblue2.png" /></></h2>
                        <br />
                        <br />

                        <form id="infoemp">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Natureza da Operação"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Regime Especial de Tributação"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Optante Simples Nacional"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="Incentivador Cultural"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text="Alíquota"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label7" runat="server" Text="Item da Lista de Serviço"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label8" runat="server" Text="Código de Tributação do Município"></asp:Label><br />
                                    </td>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label9" runat="server" Text="Município de Prestação do Serviço"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Text="Senha de acesso"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Label ID="lb10" runat="server" Text="Formato de Exportacao"></asp:Label><br />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList runat="server" ID="listaLayout"></asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <asp:Button CssClass="btClass" ID="btSalva" runat="server" Text="&#10004; Salvar" />
                                    </td>
                                    <td align="left">
                                        <asp:Button CssClass="btClass" ID="btCancela" runat="server" Text="&#10006; Cancelar" /></>
                                    
                                    </td>
                                </tr>

                            </table>
                        </form>
                    </div>
                </asp:View>

                <asp:View ID="View3" runat="server">

                    <div id="consulta">
                        <h1>Consultas
                            <img src="img/consultag.png" /></h1>
                        <br />
                        <asp:Label ID="nfse" Text="Tipos de consultas:" runat="server"></asp:Label><select id="Select1">
                            <>
                            <option>Situação de lote                             
                            </option>
                            <option>NFS-e por RPS                             
                            </option>
                            <option>Lote de RPS                             
                            </option>
                            <option>NFS-e
                            </option>

                        </select>


                        <asp:Button CssClass="btClass" ID="btConsulta" runat="server" Text="&#x1f50d; Consultar" />
                    </div>

                </asp:View>
            </asp:MultiView>
        </div>
    </form>
</body>
</html>
