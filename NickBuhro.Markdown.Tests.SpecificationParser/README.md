# NickBuhro.Markdown.Tests.SpecificationParser

This is CommonMark specification converter.
The main purpose of this project is generating class for testing Markdown converter.

P.S.: I understand that it is big overkill for such simple task and it could be done with T4 templates.
But I would like to do it because it is a good practice before writing more complex compiler.

## Architecture overview

Converter is based on the classic compiler architecture:

Source code (specification) =>
[LEXER] => 
Tokens =>
[PARSER] => 
Abstract syntax tree =>
[GENERATOR] => 
Result (C# test class)
