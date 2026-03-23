using aeroWebApi.Services;


namespace aeroWebApi.Test.Services;

public class PasswordHasherTest
{
    private readonly PasswordHasher _hasher = new PasswordHasher();
   
    [Fact]
    public void HashPassword_ShouldReturnSaltAndHashSeparatedByDelimiter()
    {
        //arrange

        var password = "test123";
        //act
        var result = _hasher.HashPassword(password);

        //assert
        Assert.Contains(":", result);
        Assert.Equal(2, result.Split(":").Length);
    }

    [Fact]
    public void VerifyPassword_CorrectPassword_ShouldReturnTrue()
    {
        //arrange

        var password = "test124";
        //act
        var hash = _hasher.HashPassword(password);
        var result = _hasher.VerifyPassword(hash, password);

        //assert
        Assert.True(result);
    }

    [Fact]
    public void VerifyPassword_WrongPassword_ShouldReturnFalse()
    {
        var password = "test124";
        var hash = _hasher.HashPassword(password);
        var result = _hasher.VerifyPassword(hash, "wrongPsw");
        Assert.False(result);
    }


}
