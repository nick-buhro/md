# NickBuhro.Markdown.Tests.SpecificationParser

This is CommonMark specification compiler.
The main purpose of this project is generating class for testing Markdown converter.

P.S.: I understand that it is greate overkill for such simple task and it could be done with T4 templates.
But I think that creating a specification parser is goog excersice.

## Architecture overview

Compiler is based on the classic compiler architecture:

Source code (specification) ->
-> [LEXER] -> Tokens ->
-> [PARSER] -> Abstract syntax tree ->
-> [GENERATOR] -> Result (C# test class)