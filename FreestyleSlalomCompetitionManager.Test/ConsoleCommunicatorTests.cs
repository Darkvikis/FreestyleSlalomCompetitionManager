using FreestyleSlalomCompetitionManager.BL;
using FreestyleSlalomCompetitionManager.BL.Enums;
using FreestyleSlalomCompetitionManager.BL.Models;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Battle;
using FreestyleSlalomCompetitionManager.BL.Models.Disciplines.Classic;

namespace FreestyleSlalomCompetitionManager.Test
{
    public class ConsoleCommunicatorTests
    {
        [Theory]
        [InlineData("classic", "senior", "men", AgeCategory.Senior, SexCategory.Man)]
        [InlineData("battle", "junior", "women", AgeCategory.Junior, SexCategory.Woman)]
        [InlineData("classic", "senior", "women", AgeCategory.Senior, SexCategory.Woman)]
        [InlineData("battle", "junior", "men", AgeCategory.Junior, SexCategory.Man)]
        public void CreateDiscipline_WhenValidInputProvided_ShouldReturnCorrectDiscipline(string input, string ageCategoryInput, string sexCategoryInput, AgeCategory expectedAgeCategory, SexCategory expectedSexCategory)
        {
            BaseDiscipline expectedDiscipline = input == "classic" ? new Classic(expectedAgeCategory, expectedSexCategory) : new Battle(expectedAgeCategory, expectedSexCategory);

            // Act
            using (var consoleInput = new ConsoleInput(ageCategoryInput, sexCategoryInput, input))
            {
                BaseDiscipline actualDiscipline = ConsoleCommunicator.CreateDiscipline();

                // Assert
                Assert.IsType(expectedDiscipline.GetType(), actualDiscipline);
                Assert.Equal(expectedAgeCategory, actualDiscipline.AgeCategory);
                Assert.Equal(expectedSexCategory, actualDiscipline.SexCategory);
            }
        }

        [Fact]
        public void CreateDiscipline_WhenInvalidInputProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "invalid";
            string ageCategoryInput = "senior";
            string sexCategoryInput = "men";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenInvalidAgeCategoryProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "classic";
            string ageCategoryInput = "invalid";
            string sexCategoryInput = "men";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenInvalidSexCategoryProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "classic";
            string ageCategoryInput = "senior";
            string sexCategoryInput = "invalid";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenEmptyInputProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "";
            string ageCategoryInput = "senior";
            string sexCategoryInput = "men";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenNullInputProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = null;
            string ageCategoryInput = "senior";
            string sexCategoryInput = "men";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenInvalidAgeCategoryAndSexCategoryProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "classic";
            string ageCategoryInput = "invalid";
            string sexCategoryInput = "invalid";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenInvalidDisciplineProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "invalid";
            string ageCategoryInput = "senior";
            string sexCategoryInput = "men";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenNullAgeCategoryAndSexCategoryProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "classic";
            string ageCategoryInput = null;
            string sexCategoryInput = null;

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenEmptyAgeCategoryAndSexCategoryProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "classic";
            string ageCategoryInput = "";
            string sexCategoryInput = "";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        [Fact]
        public void CreateDiscipline_WhenWhitespaceAgeCategoryAndSexCategoryProvided_ShouldThrowArgumentException()
        {
            // Arrange
            string input = "classic";
            string ageCategoryInput = " ";
            string sexCategoryInput = " ";

            // Act
            using (var consoleInput = new ConsoleInput(input, ageCategoryInput, sexCategoryInput))
            {
                // Assert
                Assert.Throws<ArgumentException>(() => ConsoleCommunicator.CreateDiscipline());
            }
        }

        private class ConsoleInput : IDisposable
        {
            private readonly StringReader _inputReader;
            private readonly TextWriter _originalOutput;
            private readonly StringWriter _outputWriter;

            public ConsoleInput(params string[] inputLines)
            {
                _inputReader = new StringReader(string.Join(Environment.NewLine, inputLines));
                _originalOutput = Console.Out;
                _outputWriter = new StringWriter();
                Console.SetIn(_inputReader);
                Console.SetOut(_outputWriter);
            }

            public void Dispose()
            {
                _inputReader.Dispose();
                Console.SetIn(Console.In);
                Console.SetOut(_originalOutput);
                _outputWriter.Dispose();
            }
        }
    }
}
