namespace HtmlApp

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets

type Action = Home

module Skin =
    open System.Web

    type Page =
        {
            Title : string
            Body : Content.HtmlElement list
        }

    let MainTemplate =
        Content.Template<Page>("~/Main.html")
            .With("title", fun x -> x.Title)
            .With("body", fun x -> x.Body)

    let WithTemplate title body : Content<Action> =
        Content.WithTemplate MainTemplate <| fun context ->
            {
                Title = title
                Body = body context
            }

module Site =

    let HomePage =
        Skin.WithTemplate "Taha Hachana" <| fun ctx ->
            [
                Div [new Location.Control()]
            ]

    let Main = Sitelet.Content "/" Home HomePage

[<Sealed>]
type Website() =
    interface IWebsite<Action> with
        member this.Sitelet = Site.Main
        member this.Actions = [Home]

[<assembly: Website(typeof<Website>)>]
do ()