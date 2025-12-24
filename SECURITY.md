# Security Policy

## Reporting a Vulnerability

**Please do not open a public issue for security vulnerabilities.**

Instead, please email `security@yourproject.dev` with:

1. Description of the vulnerability
2. Steps to reproduce (if applicable)
3. Impact assessment
4. Suggested fix (if you have one)

## What to Expect

- We acknowledge receipt within 48 hours
- We work on a fix within 7 days
- We notify you before public disclosure
- We credit you in the security advisory (unless you prefer anonymity)

## Security Best Practices

### For Developers

- Keep dependencies updated
- Use static analysis tools
- Follow OWASP guidelines
- Validate user input
- Use HTTPS where applicable

### For Users

- Always download from official GitHub releases
- Verify executable signatures if available
- Keep Windows and .NET updated
- Use antivirus software
- Report suspicious behavior

## Supported Versions

| Version | Supported |
|---------|-----------|
| 1.0.x   | ✅ Yes    |
| 0.9.x   | ❌ No     |
| < 0.9   | ❌ No     |

Security patches are applied to supported versions.

## Dependencies

We regularly audit our NuGet dependencies for known vulnerabilities:

```bash
dotnet package search --vulnerable
```

If a vulnerability is found:
1. We update the package immediately
2. Release a patch version
3. Notify users via security advisory

## License

This security policy is licensed under [CC0 1.0](https://creativecommons.org/publicdomain/zero/1.0/).
