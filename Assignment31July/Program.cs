using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Assignment25July
{

    public class Employee
    {
        public static int EmpCounter = 0;
        public int EmployeeUID;

        public string EmployeeName;
        public string EmployeeManagerName;
        public int empType;

        public Employee()
        {
            Console.WriteLine("Enter Employee Name");
            this.EmployeeName = Console.ReadLine();
            Console.WriteLine("Enter Employee Manager Name");
            this.EmployeeManagerName = Console.ReadLine();
            this.EmployeeUID = getEmployeeNumber();
        }

        public static int getEmployeeNumber()
        {
            EmpCounter++;
            return EmpCounter;
        }
    }

    public class ContractEmployee : Employee
    {
        public DateTime ContractDate;
        public int ContractDuration;
        public int ContractCharfe;

        public ContractEmployee() : base()
        {

            Console.WriteLine("Enter Contract date");
            this.ContractDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Contract duration");
            this.ContractDuration = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Contract Charge fee");
            this.ContractCharfe = Convert.ToInt32(Console.ReadLine());
            this.empType = 1;
        }
    }

    public class PayrollEmployee : Employee
    {
        public DateTime joiningDate;
        public int Experience;
        public float BasicSalary;
        public float DA;
        public float HRA;
        public float PF;

        public PayrollEmployee() : base()
        {
            Console.WriteLine("Enter Joining Date");
            joiningDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Experience");
            Experience = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter base Salary");
            BasicSalary = (float)Convert.ToDouble(Console.ReadLine());
            this.CalculateNetSalary();
        }

        public float CalculateNetSalary()
        {
            if (Experience >= 10)
            {
                this.DA = .10f * this.BasicSalary;
                this.HRA = 0.085f * this.BasicSalary;
                this.PF = 6200.0f;
            }
            else if (Experience >= 7 && Experience < 10)
            {
                this.DA = 0.07f * this.BasicSalary;
                this.HRA = 0.065f * this.BasicSalary;
                this.PF = 4100;
            }
            else if (Experience >= 5 && Experience < 7)
            {
                this.DA = 0.041f * this.BasicSalary;
                this.HRA = 0.038f * this.BasicSalary;
                this.PF = 1800;
            }
            else
            {
                this.DA = 0.019f * this.BasicSalary;
                this.HRA = 0.02f * this.BasicSalary;
                this.PF = 1200;
            }

            return this.BasicSalary + this.DA + this.HRA - this.PF;
        }
    }

    public class DataBase
    {
        public List<Employee> EmployeeList = new List<Employee>();
    }
    internal class q1
    {
        static void Main(String[] args)
        {
            DataBase db = new DataBase();

            while (true)
            {
                int respg1 = 0;
                int respg2 = 0;
                Console.WriteLine("1. Enter 1 to add an employee\n2. Enter 2 to show All Employees\n3. to delete an employee record\n4. Update a record\n5. Any other key to exit");
                try
                {
                    respg1 = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    break;
                }
                if (respg1 == 1)
                {

                    Console.WriteLine("1. Enter 1 to create Contract type employee\n2. Enter 2 to create Payroll type employee\n3. Any other key to exit");
                    respg2 = Convert.ToInt32(Console.ReadLine());


                    if (respg2 == 1)
                    {
                        ContractEmployee cont = new ContractEmployee();

                        string ConnectingString = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                        SqlConnection connection = new SqlConnection(ConnectingString);
                        SqlCommand command = new SqlCommand();
                        command.CommandText = $"INSERT INTO employee (employeeName, employeeManager, empType, contractDate, contractDuration)VALUES('{cont.EmployeeName}', '{cont.EmployeeManagerName}', {(int)cont.empType}, '{cont.ContractCharfe}', {(int)cont.ContractDuration})";
                        command.Connection = connection;
                        
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                            command.Dispose();
                            connection.Dispose();
                        }
                    }
                    else if (respg2 == 2)
                    {
                        PayrollEmployee prl = new PayrollEmployee();

                        string ConnectingString = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                        SqlConnection connection = new SqlConnection(ConnectingString);
                        SqlCommand command = new SqlCommand();
                        command.CommandText = $"INSERT INTO employee (employeeName, employeeManager, empType, joiningDate, experience, basicSalary, da, hra, pf)VALUES('{prl.EmployeeName}', '{prl.EmployeeManagerName}', 2, '{prl.joiningDate}', {prl.Experience}, {prl.BasicSalary}, {prl.DA}, {prl.HRA}, {prl.PF})";
                        command.Connection = connection;

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                            command.Dispose();
                            connection.Dispose();
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                else if (respg1 == 2)
                {
                    string ConnectingString = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                    SqlConnection connection = new SqlConnection(ConnectingString);
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"select * from employee";
                    command.Connection = connection;

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if(reader.HasRows)
                            while(reader.Read())
                            {
                                for(int i=0; i<reader.FieldCount; i++) {
                                    Console.Write(reader.GetName(i) + ":" + reader[i]+"   ");
                                }
                                Console.WriteLine();
                            }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                        command.Dispose();
                        connection.Dispose();
                    }


                    /*                    foreach (var l in db.EmployeeList)
                                        {
                                            if (l is ContractEmployee)
                                            {
                                                Console.WriteLine($"{l.EmployeeUID}\t{l.EmployeeName}\t{l.EmployeeManagerName}\t");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"{l.EmployeeUID}\t{l.EmployeeName}\t{l.EmployeeManagerName}\t");

                                            }

                                        }*/

                }
                else if (respg1 == 3)
                {
                    Console.WriteLine("Enter UID of Record to delete");
                    try
                    {
                        respg2 = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        break;
                    }
                    string ConnectingString = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                    SqlConnection connection = new SqlConnection(ConnectingString);
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"delete from employee where (employeeUID={respg2})";
                    command.Connection = connection;

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                        command.Dispose();
                        connection.Dispose();
                    }

                }
                else if(respg1 == 4)
                {
                    Console.WriteLine("Enter UID of Record to update");
                    try
                    {
                        respg2 = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        break;
                    }
                    string ConnectingString = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                    SqlConnection connection = new SqlConnection(ConnectingString);
                    SqlCommand command = new SqlCommand();
                    command.CommandText = $"select * from employee where (employeeUID={respg2})";
                    command.Connection = connection;

                    try
                    {
                        connection.Open();
                        SqlDataReader  reader = command.ExecuteReader();
                        int emptypeRes = 0;
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                for(int i=0; i<reader.FieldCount; i++)
                                {
                                    if (reader.GetName(i) == "empType")
                                    {
                                        emptypeRes = (int)reader[i];

                                    }

                                    Console.WriteLine(reader.GetName(i) + ":" + reader[i] );
                                }
                            }
                        }
                        connection.Close();
                        command.Dispose();
                        connection.Dispose();
                        if (emptypeRes == 1)
                        {
                            ContractEmployee cont = new ContractEmployee();

                            string ConnectingString1 = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                            SqlConnection connection1 = new SqlConnection(ConnectingString1);
                            SqlCommand command1 = new SqlCommand();
                            command1.CommandText = $"UPDATE employee SET employeeName='{cont.EmployeeName}', employeeManager='{cont.EmployeeManagerName}', empType={(int)cont.empType}, contractDate='{cont.ContractDate}', contractDuration={(int)cont.ContractDuration} where employeeUID = {respg2} ";
                            command1.Connection = connection1;

                            try
                            {
                                connection1.Open();
                                command1.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                connection1.Close();
                                command1.Dispose();
                                connection1.Dispose();
                            }

                        }
                        else
                        {
                            PayrollEmployee prl = new PayrollEmployee();

                            string ConnectingString2 = @"Data Source=DotNetFSD\SQLEXPRESS;Initial Catalog=emp;User id=sa;Password=pass@123;";
                            SqlConnection connection2 = new SqlConnection(ConnectingString2);
                            SqlCommand command2 = new SqlCommand();
                            command2.CommandText = $"UPDATE employee SET employeeName='{prl.EmployeeName}', employeeManager= '{prl.EmployeeManagerName}', empType=2, joiningDate='{prl.joiningDate}', experience={prl.Experience}, basicSalary={prl.BasicSalary}, da={prl.DA}, hra={prl.HRA}, pf={prl.PF} where employeeUID = {respg2}";
                            command.Connection = connection;

                            try
                            {
                                connection2.Open();
                                command2.ExecuteNonQuery();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                connection2.Close();
                                command2.Dispose();
                                connection2.Dispose();
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }
                else
                {
                    break;
                }
            }
        }
    }
}
