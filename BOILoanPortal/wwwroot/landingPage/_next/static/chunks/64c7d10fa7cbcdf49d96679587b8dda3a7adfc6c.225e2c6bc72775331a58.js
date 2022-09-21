(window.webpackJsonp_N_E = window.webpackJsonp_N_E || []).push([
    [3], {
        ODXe: function(a, e, l) {
            "use strict";

            function n(a, e) {
                (null == e || e > a.length) && (e = a.length);
                for (var l = 0, n = new Array(e); l < e; l++) n[l] = a[l];
                return n
            }

            function s(a, e) {
                return function(a) {
                    if (Array.isArray(a)) return a
                }(a) || function(a, e) {
                    if ("undefined" !== typeof Symbol && Symbol.iterator in Object(a)) {
                        var l = [],
                            n = !0,
                            s = !1,
                            t = void 0;
                        try {
                            for (var i, c = a[Symbol.iterator](); !(n = (i = c.next()).done) && (l.push(i.value), !e || l.length !== e); n = !0);
                        } catch (r) {
                            s = !0, t = r
                        } finally {
                            try {
                                n || null == c.return || c.return()
                            } finally {
                                if (s) throw t
                            }
                        }
                        return l
                    }
                }(a, e) || function(a, e) {
                    if (a) {
                        if ("string" === typeof a) return n(a, e);
                        var l = Object.prototype.toString.call(a).slice(8, -1);
                        return "Object" === l && a.constructor && (l = a.constructor.name), "Map" === l || "Set" === l ? Array.from(a) : "Arguments" === l || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(l) ? n(a, e) : void 0
                    }
                }(a, e) || function() {
                    throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.")
                }()
            }
            l.d(e, "a", (function() {
                return s
            }))
        },
        SxW8: function(a, e, l) {
            "use strict";
            var n = l("ODXe"),
                s = l("q1tI"),
                t = l.n(s);

            function i(a, e) {
                if (null == a) return {};
                var l, n, s = function(a, e) {
                    if (null == a) return {};
                    var l, n, s = {},
                        t = Object.keys(a);
                    for (n = 0; n < t.length; n++) l = t[n], e.indexOf(l) >= 0 || (s[l] = a[l]);
                    return s
                }(a, e);
                if (Object.getOwnPropertySymbols) {
                    var t = Object.getOwnPropertySymbols(a);
                    for (n = 0; n < t.length; n++) l = t[n], e.indexOf(l) >= 0 || Object.prototype.propertyIsEnumerable.call(a, l) && (s[l] = a[l])
                }
                return s
            }
            var c = l("nOHt"),
                r = l("YFqc"),
                o = l.n(r),
                m = t.a.createElement,
                v = Object(c.withRouter)((function(a) {
                    var e = a.router,
                        l = a.children,
                        n = i(a, ["router", "children"]),
                        c = s.Children.only(l),
                        r = c.props.className || "";
                    return e.pathname === n.href && n.activeClassName && (r = "".concat(r, " ").concat(n.activeClassName).trim()), delete n.activeClassName, m(o.a, n, t.a.cloneElement(c, {
                        className: r
                    }))
                })),
                u = t.a.createElement;
            e.a = function() {
                var a = t.a.useState(!0),
                    e = Object(n.a)(a, 2),
                    l = e[0],
                    s = e[1],
                    i = t.a.useState(!1),
                    c = Object(n.a)(i, 2),
                    r = c[0],
                    o = c[1],
                    m = function() {
                        o(!r)
                    },
                    f = function() {
                        s(!l)
                    };
                t.a.useEffect((function() {
                    var a = document.getElementById("navbar");
                    document.addEventListener("scroll", (function() {
                        window.scrollY > 170 ? a.classList.add("is-sticky") : a.classList.remove("is-sticky")
                    })), window.scrollTo(0, 0)
                }));
                var N = l ? "collapse navbar-collapse" : "collapse navbar-collapse show",
                    d = l ? "navbar-toggler navbar-toggler-right collapsed" : "navbar-toggler navbar-toggler-right";
                return u(t.a.Fragment, null, u("div", {
                    id: "navbar",
                    className: "navbar-area"
                }, u("div", {
                    className: "main-navbar"
                }, u("div", {
                    className: "container-fluid"
                }, u("nav", {
                    className: "navbar navbar-expand-lg navbar-light"
                }, u(v, {
                    href: "/"
                }, u("a", {
                    onClick: f,
                    className: "navbar-brand"
                }, u("img", {
                    src: "/images/logo.png",
                    alt: "logo"
                }))), u("button", {
                    onClick: f,
                    className: d,
                    type: "button",
                    "data-toggle": "collapse",
                    "data-target": "#navbarSupportedContent",
                    "aria-controls": "navbarSupportedContent",
                    "aria-expanded": "false",
                    "aria-label": "Toggle navigation"
                }, u("span", {
                    className: "icon-bar top-bar"
                }), u("span", {
                    className: "icon-bar middle-bar"
                }), u("span", {
                    className: "icon-bar bottom-bar"
                })), u("div", {
                    className: N,
                    id: "navbarSupportedContent"
                }, u("ul", {
                    className: "navbar-nav"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "#",
                    activeClassName: "active"
                }, u("a", {
                    onClick: function(a) {
                        return a.preventDefault()
                    },
                    className: "nav-link"
                }, "Home ", u("i", {
                    className: "bx bx-chevron-down"
                }))), u("ul", {
                    className: "dropdown-menu"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Home Demo One"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/index-2",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Home Demo Two"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/index-3",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Home Demo Three"))))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "#"
                }, u("a", {
                    onClick: function(a) {
                        return a.preventDefault()
                    },
                    className: "nav-link"
                }, "Pages ", u("i", {
                    className: "bx bx-chevron-down"
                }))), u("ul", {
                    className: "dropdown-menu"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/about-us",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "About Us"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/team",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Team"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/gallery",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Gallery"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/loan-calculator",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Loan Calculator"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/apply-now",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Apply now"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/faq",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "FAQ"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/testimonials",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Testimonials"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "#"
                }, u("a", {
                    onClick: function(a) {
                        return a.preventDefault()
                    },
                    className: "nav-link"
                }, "Projects ", u("i", {
                    className: "bx bx-chevron-down"
                }))), u("ul", {
                    className: "dropdown-menu"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/projects",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Projects"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/project-details",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Project Details"))))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "#"
                }, u("a", {
                    onClick: function(a) {
                        return a.preventDefault()
                    },
                    className: "nav-link"
                }, "User ", u("i", {
                    className: "bx bx-chevron-down"
                }))), u("ul", {
                    className: "dropdown-menu"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/sign-in",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Sign In"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/sign-up",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Sign Up"))))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/terms-conditions",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Terms & Conditions"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/privacy-policy",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Privacy Policy"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/404",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "404 Error Page"))))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "#"
                }, u("a", {
                    onClick: function(a) {
                        return a.preventDefault()
                    },
                    className: "nav-link"
                }, "Services ", u("i", {
                    className: "bx bx-chevron-down"
                }))), u("ul", {
                    className: "dropdown-menu"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/services",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Services"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/services-details",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Services Details"))))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/funding",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Funding"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/loans",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Loans"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "#"
                }, u("a", {
                    onClick: function(a) {
                        return a.preventDefault()
                    },
                    className: "nav-link"
                }, "News ", u("i", {
                    className: "bx bx-chevron-down"
                }))), u("ul", {
                    className: "dropdown-menu"
                }, u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/news",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "News"))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/news-details",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "News Details"))))), u("li", {
                    className: "nav-item"
                }, u(v, {
                    href: "/contact",
                    activeClassName: "active"
                }, u("a", {
                    onClick: f,
                    className: "nav-link"
                }, "Contact")))), u("div", {
                    className: "others-options d-flex align-items-center"
                }, u("div", {
                    className: "option-item"
                }, u("i", {
                    className: "search-btn flaticon-magnifying-glass",
                    onClick: function(a) {
                        a.preventDefault(), m()
                    }
                })), u("div", {
                    className: "option-item"
                }, u("div", {
                    className: "info"
                }, u("i", {
                    className: "flaticon-telephone"
                }), u("span", null, "Call Now"), u("p", null, u("a", {
                    href: "tel:1514312-5678"
                }, "+234 (514) 312-5678")))), u("div", {
                    className: "option-item"
                }, u(v, {
                    href: "/register"
                }, u("a", {
                    className: "default-btn"
                }, "Register/Login", u("span", null)))) )))))), u("div", {
                    className: "search-overlay ".concat(r ? "search-overlay-active" : null)
                }, u("div", {
                    className: "d-table"
                }, u("div", {
                    className: "d-table-cell"
                }, u("div", {
                    className: "search-overlay-layer"
                }), u("div", {
                    className: "search-overlay-layer"
                }), u("div", {
                    className: "search-overlay-layer"
                }), u("div", {
                    className: "search-overlay-close",
                    onClick: function(a) {
                        a.preventDefault(), m()
                    }
                }, u("span", {
                    className: "search-overlay-close-line"
                }), u("span", {
                    className: "search-overlay-close-line"
                })), u("div", {
                    className: "search-overlay-form"
                }, u("form", null, u("input", {
                    type: "text",
                    className: "input-search",
                    placeholder: "Search here..."
                }), u("button", {
                    type: "submit"
                }, u("i", {
                    className: "bx bx-search"
                }))))))))
            }
        },
        YFqc: function(a, e, l) {
            a.exports = l("cTJO")
        },
        cTJO: function(a, e, l) {
            "use strict";
            var n = l("J4zp"),
                s = l("284h");
            e.__esModule = !0, e.default = void 0;
            var t, i = s(l("q1tI")),
                c = l("elyg"),
                r = (l("g/15"), l("nOHt")),
                o = new Map,
                m = window.IntersectionObserver,
                v = {};
            var u = function(a, e) {
                var l = t || (m ? t = new m((function(a) {
                    a.forEach((function(a) {
                        if (o.has(a.target)) {
                            var e = o.get(a.target);
                            (a.isIntersecting || a.intersectionRatio > 0) && (t.unobserve(a.target), o.delete(a.target), e())
                        }
                    }))
                }), {
                    rootMargin: "200px"
                }) : void 0);
                return l ? (l.observe(a), o.set(a, e), function() {
                    try {
                        l.unobserve(a)
                    } catch (e) {
                        console.error(e)
                    }
                    o.delete(a)
                }) : function() {}
            };

            function f(a, e, l, n) {
                (0, c.isLocalURL)(e) && (a.prefetch(e, l, n).catch((function(a) {
                    0
                })), v[e + "%" + l] = !0)
            }
            var N = function(a) {
                var e = !1 !== a.prefetch,
                    l = i.default.useState(),
                    s = n(l, 2),
                    t = s[0],
                    o = s[1],
                    N = (0, r.useRouter)(),
                    d = N && N.pathname || "/",
                    p = i.default.useMemo((function() {
                        var e = (0, c.resolveHref)(d, a.href);
                        return {
                            href: e,
                            as: a.as ? (0, c.resolveHref)(d, a.as) : e
                        }
                    }), [d, a.href, a.as]),
                    h = p.href,
                    g = p.as;
                i.default.useEffect((function() {
                    if (e && m && t && t.tagName && (0, c.isLocalURL)(h) && !v[h + "%" + g]) return u(t, (function() {
                        f(N, h, g)
                    }))
                }), [e, t, h, g, N]);
                var b = a.children,
                    k = a.replace,
                    y = a.shallow,
                    C = a.scroll;
                "string" === typeof b && (b = i.default.createElement("a", null, b));
                var w = i.Children.only(b),
                    x = {
                        ref: function(a) {
                            a && o(a), w && "object" === typeof w && w.ref && ("function" === typeof w.ref ? w.ref(a) : "object" === typeof w.ref && (w.ref.current = a))
                        },
                        onClick: function(a) {
                            w.props && "function" === typeof w.props.onClick && w.props.onClick(a), a.defaultPrevented || function(a, e, l, n, s, t, i) {
                                ("A" !== a.currentTarget.nodeName || ! function(a) {
                                    var e = a.currentTarget.target;
                                    return e && "_self" !== e || a.metaKey || a.ctrlKey || a.shiftKey || a.altKey || a.nativeEvent && 2 === a.nativeEvent.which
                                }(a) && (0, c.isLocalURL)(l)) && (a.preventDefault(), null == i && (i = n.indexOf("#") < 0), e[s ? "replace" : "push"](l, n, {
                                    shallow: t
                                }).then((function(a) {
                                    a && i && (window.scrollTo(0, 0), document.body.focus())
                                })))
                            }(a, N, h, g, k, y, C)
                        }
                    };
                return e && (x.onMouseEnter = function(a) {
                    (0, c.isLocalURL)(h) && (w.props && "function" === typeof w.props.onMouseEnter && w.props.onMouseEnter(a), f(N, h, g, {
                        priority: !0
                    }))
                }), !a.passHref && ("a" !== w.type || "href" in w.props) || (x.href = (0, c.addBasePath)(g)), i.default.cloneElement(w, x)
            };
            e.default = N
        },
        mdYk: function(a, e, l) {
            "use strict";
            var n = l("q1tI"),
                s = l.n(n),
                t = l("YFqc"),
                i = l.n(t),
                c = s.a.createElement;
            e.a = function() {
                var a = (new Date).getFullYear();
                return c(s.a.Fragment, null, c("footer", {
                    className: "footer-area pt-100 pb-70"
                }, c("div", {
                    className: "container"
                }, c("div", {
                    className: "row"
                }, c("div", {
                    className: "col-lg-3 col-md-6"
                }, c("div", {
                    className: "single-footer-widget"
                }, c("div", {
                    className: "logo"
                }, c(i.a, {
                    href: "/"
                }, c("a", null, c("img", {
                    src: "/images/logo-white.png",
                    alt: "image"
                })))), c("p", null, "Lorem ipsum dolor sit amet, consec tetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."), c("ul", {
                    className: "social"
                }, c("li", null, c("b", null, "Follow us:")), c("li", null, c("a", {
                    href: "#",
                    target: "_blank"
                }, c("i", {
                    className: "flaticon-twitter"
                }))), c("li", null, c("a", {
                    href: "#",
                    target: "_blank"
                }, c("i", {
                    className: "flaticon-instagram"
                }))), c("li", null, c("a", {
                    href: "#",
                    target: "_blank"
                }, c("i", {
                    className: "flaticon-facebook"
                }))), c("li", null, c("a", {
                    href: "#",
                    target: "_blank"
                }, c("i", {
                    className: "flaticon-linkedin"
                })))))), c("div", {
                    className: "col-lg-3 col-md-6"
                }, c("div", {
                    className: "single-footer-widget"
                }, c("h3", null, "Quick Links"), c("ul", {
                    className: "quick-links"
                }, c("li", null, c(i.a, {
                    href: "/about-us"
                }, c("a", null, "About"))), c("li", null, c(i.a, {
                    href: "#"
                }, c("a", null, "Our Performance"))), c("li", null, c(i.a, {
                    href: "/faq"
                }, c("a", null, "Help (FAQ)"))), c("li", null, c(i.a, {
                    href: "/news"
                }, c("a", null, "Blog"))), c("li", null, c(i.a, {
                    href: "/contact"
                }, c("a", null, "Contact")))))), c("div", {
                    className: "col-lg-3 col-md-6"
                }, c("div", {
                    className: "single-footer-widget"
                }, c("h3", null, "Other Resources"), c("ul", {
                    className: "quick-links"
                }, c("li", null, c(i.a, {
                    href: "/contact"
                }, c("a", null, "Support"))), c("li", null, c(i.a, {
                    href: "/privacy-policy"
                }, c("a", null, "Privacy Policy"))), c("li", null, c(i.a, {
                    href: "/terms-condition"
                }, c("a", null, "Terms of Service"))), c("li", null, c(i.a, {
                    href: "#"
                }, c("a", null, "Business Loans"))), c("li", null, c(i.a, {
                    href: "#"
                }, c("a", null, "Loan Services")))))), c("div", {
                    className: "col-lg-3 col-md-6"
                }, c("div", {
                    className: "single-footer-widget"
                }, c("h3", null, "Contact Us"), c("div", {
                    className: "info-contact"
                }, c("i", {
                    className: "flaticon-pin"
                }), c("span", null, "6890 Blvd, The Bronx, NY 1058 New York, USA")), c("div", {
                    className: "info-contact"
                }, c("i", {
                    className: "flaticon-mail"
                }), c("span", null, c("a", {
                    href: "mailto:hello@leza.com"
                }, "hello@leza.com")), c("span", null, c("a", {
                    href: "#"
                }, "Skype: example"))), c("div", {
                    className: "info-contact"
                }, c("i", {
                    className: "flaticon-telephone"
                }), c("span", null, c("a", {
                    href: "tel:1514312-6688"
                }, "+1 (514) 312-5678")), c("span", null, c("a", {
                    href: "tel:1514312-6688"
                }, "+1 (514) 312-6688")))))))), c("div", {
                    className: "copy-right-area"
                }, c("div", {
                    className: "container"
                }, c("div", {
                    className: "copy-right-content"
                }, c("p", null, "Copyright @", a, " Leza. Designed By ", c("a", {
                    href: "https://envytheme.com/",
                    target: "_blank"
                }, "EnvyTheme"))))))
            }
        }
    }
]);