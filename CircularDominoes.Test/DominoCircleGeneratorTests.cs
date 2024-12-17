namespace CircularDominoes.Test;

public class DominoCircleGeneratorTests
{
    [Theory]
    [MemberData(nameof(ValidDominoChains))]
    public void FindCircularChain_When_valid_input_Then_result_is_a_valid_circle(List<Domino> dominoes, string expectedResult)
    {
        //Arrange
        var circleGenerator = new DominoCircleGenerator(dominoes);

        //Act
        var result = circleGenerator.FindCircularChain();
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedResult, result.Value);
    }

    public static TheoryData<List<Domino>, string> ValidDominoChains()
    {
        return new TheoryData<List<Domino>, string>
        {
            { [new(2, 1), new(2, 3), new(1, 3)], "[2|1] [1|3] [3|2]" },
            { [new(1, 2), new(2, 3), new(3, 1)], "[1|2] [2|3] [3|1]" },
            { [new(1, 4), new(4, 5), new(5, 2), new(2, 1)], "[1|4] [4|5] [5|2] [2|1]" },
            { [new(4, 5), new(1, 4), new(2, 1), new(5, 2)], "[4|5] [5|2] [2|1] [1|4]" },
            { [new(1, 1)], "[1|1]"}
        };
    }
    
    [Theory]
    [MemberData(nameof(InvalidDominoChains))]
    public void FindCircularChain_When_invalid_input_Then_result_is_error(List<Domino> dominoes)
    {
        //Arrange
        var circleGenerator = new DominoCircleGenerator(dominoes);

        //Act
        var result = circleGenerator.FindCircularChain();
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Equal("No circular domino chain is possible.", result.Error.Description);
    }
    
    public static TheoryData<List<Domino>> InvalidDominoChains()
    {
        return 
        [
            new List<Domino> { new(1, 2), new(4, 1), new(2, 3) },
            new List<Domino> { new(1, 2), new(4, 5), new(6, 3) },
            new List<Domino> { new(1, 3) }
        ];
    }
    
    [Fact]
    public void FindCircularChain_When_empty_dominoes_input_Then_result_is_error()
    {
        //Arrange
        var circleGenerator = new DominoCircleGenerator([]);

        //Act
        var result = circleGenerator.FindCircularChain();
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Equal("There aren't any dominoes to create a circle with", result.Error.Description);
    }
}