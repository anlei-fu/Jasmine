﻿namespace Jasmine.Parsers.Html
{
    /// <summary>
    /// use to mark an element is start-part or end-part
    /// </summary>
    public enum StartOrEnd
    {
        End,
        Start,
        Unknow,
    }

    /// <summary>
    /// use to mark an elemnt is single tag of double tag
    /// </summary>
    public enum SingleOrDouble
    {
        Double, Single, Unknow,
    }
    /// <summary>
    /// use to mark an element type
    /// </summary>
    public enum ElementType
    {
        Annotation,
        Doctype,
        A,
        Abbr,
        Address,
        Applet,
        Acronym,
        Area,
        Article,
        Aside,
        Audio,
        B,
        Bese,
        BaseFont,
        Bdi,
        Bdo,
        Big,
        BlockQuote,
        Body,
        Br,
        Button,
        Canvas,
        Caption,
        Center,
        Cite,
        Code,
        Col,
        ColGroup,
        Command,
        DataList,
        Dd,
        Del,
        Details,
        Dfn,
        Dir,
        Div,
        Dl,
        Dt,
        Em,
        Embed,
        FieldSet,
        Figcaption,
        Figure,
        Font,
        Footer,
        Form,
        Frame,
        FrameSet,
        H1,
        H2,
        H3,
        H4,
        H5,
        H6,
        Head,
        Header,
        Hgroup,
        Hr,
        Html,
        I,
        Iframe,
        Img,
        Input,
        Ins,
        KeyGen,
        Kbd,
        Label,
        Legend,
        Li,
        Link,
        Map,
        Mark,
        Menu,
        Meta,
        Meter,
        Nav,
        Noframes,
        Noscript,
        Abject,
        Ol,
        OptGroup,
        Option,
        Output,
        P,
        Param,
        Pre,
        Progress,
        Q,
        Rp,
        Ruby,
        S,
        Samp,
        Script,
        Select,
        Small,
        Source,
        Span,
        Strike,
        Strong,
        Style,
        Sub,
        Summary,
        Sup,
        Table,
        TBody,
        Td,
        TextArea,
        TFoot,
        Th,
        Thead,
        Time,
        Title,
        Tr,
        Track,
        Tt,
        U,
        Ul,
        Var,
        Video,
        Wbr,
        Rt,
        Section,
        Unknow
    }
}