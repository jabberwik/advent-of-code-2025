# Advent of Code 2025

These are my solutions for the [Advent of Code](https://adventofcode.com/2025).

Solutions are written in C# using .NET 10. Parts 1 and 2 run together.

## Setup 

Install and activate [mise-en-place](https://mise.jdx.dev).

Paste your AOC session token in a local `mise.local.toml` file. Grab it from the site cookie in your browser dev tools.
```
# mise.local.toml

[env]
AOC_SESSION = "your-session-token"
```

## Tasks

```sh
# Set up the project template for a day
mise prep {day-no}

# Fetch the input file for a given day
mise fetch {day-no}

# Run tests for a given day
mise test {day-no}

# Run the solution code for a given day
mise go {day-no}
```