# Contributing to Verse Visual Blueprint Editor

## Welcome!

We're excited that you're interested in contributing to the Verse Visual Blueprint Editor! This guide will help you understand our development process and how to contribute effectively.

## Code of Conduct

Be respectful, inclusive, and professional. We're building a welcoming community for all developers.

## Getting Started

### For Bug Reports

1. **Search existing issues** to avoid duplicates
2. **Create a detailed bug report** including:
   - Steps to reproduce
   - Expected behavior
   - Actual behavior
   - Environment (Windows version, .NET version, etc.)
   - Screenshots if applicable

### For Feature Requests

1. **Check discussions** for similar ideas
2. **Open a discussion** before implementing major features
3. **Describe the use case** and benefit to the community

### For Code Contributions

1. **Fork the repository**
2. **Create a feature branch**:
   ```bash
   git checkout -b feature/amazing-feature
   ```
3. **Make your changes** following our code style
4. **Write or update tests**
5. **Commit with clear messages**:
   ```bash
   git commit -m "Add amazing feature"
   ```
6. **Push to your fork**:
   ```bash
   git push origin feature/amazing-feature
   ```
7. **Open a Pull Request**

## Development Workflow

### Setting Up

See [DEVELOPMENT.md](DEVELOPMENT.md) for detailed setup instructions.

### Code Style

- Follow C# coding conventions
- Use the provided `.editorconfig` for formatting
- Run `dotnet format` before committing
- Maximum line length: 120 characters
- Use meaningful variable/method names

### Commit Messages

```
[TYPE] Brief description

Longer explanation if needed. Reference issues with #123.

Types:
- feat: New feature
- fix: Bug fix
- docs: Documentation
- style: Code style
- refactor: Code refactoring
- test: Tests
- chore: Build, dependencies
```

### Pull Request Process

1. **Update documentation** if needed
2. **Add tests** for new features
3. **Ensure all tests pass**: `dotnet test`
4. **Keep commits focused** and logical
5. **Respond to review feedback** promptly

## Areas for Contribution

### High Priority

- [ ] Visual node graph canvas implementation
- [ ] Improved node connection UI
- [ ] Advanced Verse syntax support
- [ ] Blueprint templates library
- [ ] Performance optimizations

### Medium Priority

- [ ] Additional Verse digest parsing
- [ ] Export validation and error checking
- [ ] Keyboard shortcuts
- [ ] Undo/Redo system
- [ ] Multi-file support

### Welcome Contributions

- Bug fixes
- Documentation improvements
- Code examples
- Test coverage
- Performance improvements
- UI/UX enhancements

## Testing Guidelines

### Test Structure

```csharp
[TestClass]
public class FeatureTests
{
    [TestMethod]
    [TestCategory("Feature")]
    public void ShouldDoSomething_WhenConditionMet()
    {
        // Arrange
        var input = SetupTestData();

        // Act
        var result = MethodUnderTest(input);

        // Assert
        Assert.IsTrue(result.IsValid);
    }
}
```

### Running Tests

```bash
# All tests
dotnet test

# Specific category
dotnet test --filter "TestCategory=Feature"

# With coverage (requires OpenCover)
dotnet test /p:CollectCoverage=true
```

## Documentation

- Update README.md for user-facing changes
- Update DEVELOPMENT.md for developer-facing changes
- Add XML comments to public APIs
- Include examples in code comments

Example XML comment:
```csharp
/// <summary>
/// Parses a Verse digest file and extracts classes.
/// </summary>
/// <param name="filePath">Path to the .digest.verse file</param>
/// <returns>Parsed digest containing all Verse classes</returns>
/// <exception cref="FileNotFoundException">Thrown if file doesn't exist</exception>
public VerseDiagent ParseDigestFile(string filePath)
{
    // Implementation
}
```

## Review Process

Our maintainers will:

1. Review your code for quality and style
2. Suggest improvements if needed
3. Run automated tests
4. Approve and merge when ready

Be patient and respectful during review. We're all volunteers!

## Release Process

Releases happen quarterly. To have your changes included:

1. Submit PR before the release cutoff
2. Get approval from at least one maintainer
3. Your contribution will be included in the next release

## Community

- **Discussions**: Share ideas and ask questions
- **Issues**: Report bugs and request features
- **Discord**: Join our community server (coming soon)

## Recognition

Contributors will be recognized in:
- Release notes
- README contributors section
- GitHub contributors page

## Questions?

- Check [DEVELOPMENT.md](DEVELOPMENT.md)
- Open a [Discussion](../../discussions)
- Comment on related [Issues](../../issues)

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

**Thank you for contributing to Verse Visual Blueprint Editor! 🙏**
