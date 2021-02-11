namespace Task2
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }

}









//1)
//CREATE PROCEDURE[dbo].[GetCarsByID_1]
//@paramId int,
// @Id int out,
// @Name varchar(25) out,
// @Price int out
// AS
// SELECT @ID = c.Id, @Name = c.[Name], @Price = c.Price FROM Cars as c
// WHERE c.Id = @paramId
// GO


//2)
//Create PROCEDURE[dbo].[GetCarsByID_2]
//@paramId int
//AS
// SELECT* FROM Cars as c
// WHERE c.Id = @paramId
// GO

// exec GetCarsByID_2 10
// GO
