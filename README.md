# Readme

[![Deploy to GitHub Pages](https://github.com/davewhiteford/Financr/actions/workflows/main.yml/badge.svg)](https://github.com/davewhiteford/Financr/actions/workflows/main.yml)

Demo deployed and [Hosted on github pages](https://davewhiteford.github.io/Financr/).

Can be installed as a PWA.

It can:
 - Calculate Loan/Mortgage payments and render a graph for amortization (using MudBlazor or Plotly charts)
 - Calculate Savings/Pension returns:
   - accounting for compound interest 
   - monthly deposits 
   - annual increases to monthly deposits.
   - Render Graphs of this over time using MudBlazor or Plotly charts

Heavily inspired by [House Price Calculator, here](https://github.com/aahendry/HousePriceCalculator)

# Tech
 - Net7
 - Blazor Wasm (PWA)
 - MudBlazor
 - NUnit
 - FluentAssertions
 - [Plotly.Blazor](https://github.com/LayTec-AG/Plotly.Blazor)

# Running locally

Clone, open in VS and F5

`dotnet watch run debug --project Financr` for live reload

# Screenshot

![image](https://user-images.githubusercontent.com/87719153/217836240-c28cea8b-77d7-4956-b0e0-6178c29b0e1a.png)

