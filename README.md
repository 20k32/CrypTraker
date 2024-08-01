# CrypTraker test application

The following technologies were used:
- WPF platform
- CoinCap as api & HttpClient for interaction.
- Caliburn.Micro as MVVM Framework.
- DI (used Caliburn.Micro's IoC Container implementation)

|          Feature          |       Status       |
|---------------------------|--------------------|
| Currency converter        | :heavy_check_mark: |
| Light/Dark theme          | :heavy_check_mark: |
| Multiple localizations    | :heavy_check_mark: |
| Charts                    | :heavy_multiplication_x: |


Program Description
- Multi-page application with navigation.
- Main page called "Top currencies", it displays top 20 currencies returned by API, it also supports pagination. 
- Detailed info page called "Detailed info", it displays currency name, alias, price, volume, price change in 24 hours & top 10 markets in which related currency can be purchased. You can also go to the market page in browser from application.
- You can search currency by it's name (Bitcoin) or alias (btc) from main page (Top currencies). Search displays top 100 results.
- Page for currency converter called "Currency converter" first you need to select currency.
- Page for changing localization and themes called "Settings".

How can I view detailed info for currency?
> All you need is to navigate to "Top currencies" page, select a currency by left mouse button, and click one more time with your left mouse button (do double click in other words) than navigate to "Detailed info" page.

How can I convert one currency to another?
> First you need to navigate to "Top currencies" page, than select a currency by left mouse button (than press right mouse button) or right mouse button. If you select currency by right mouse button you immediately will see a menu with two options:
> - Add: adds the currency to converter list.
> - Remove: removes the currency from converter list.
>
> When you add your first currency to converter list you will see a small menu to the right of the table. 
> When you add two options to converter list - you can go to tab "Currency converter" to convert currencies (or press button from menu, this button will navigate you to "Currency converter" page automatically).