(function () {
	var h,
	k = this;
	function l(a) {
		return "string" == typeof a
	}
	function m(a) {
		a = a.split(".");
		for (var b = k, c = 0; c < a.length; c++)
			if (b = b[a[c]], null == b)
				return null;
		return b
	}
	function aa() {}
	function p(a) {
		var b = typeof a;
		if ("object" == b)
			if (a) {
				if (a instanceof Array)
					return "array";
				if (a instanceof Object)
					return b;
				var c = Object.prototype.toString.call(a);
				if ("[object Window]" == c)
					return "object";
				if ("[object Array]" == c || "number" == typeof a.length && "undefined" != typeof a.splice && "undefined" != typeof a.propertyIsEnumerable && !a.propertyIsEnumerable("splice"))
					return "array";
				if ("[object Function]" == c || "undefined" != typeof a.call && "undefined" != typeof a.propertyIsEnumerable && !a.propertyIsEnumerable("call"))
					return "function"
			} else
				return "null";
		else if ("function" == b && "undefined" == typeof a.call)
			return "object";
		return b
	}
	function q(a) {
		var b = typeof a;
		return "object" == b && null != a || "function" == b
	}
	var r = "closure_uid_" + (1E9 * Math.random() >>> 0),
	t = 0;
	function ba(a, b, c) {
		return a.call.apply(a.bind, arguments)
	}
	function ca(a, b, c) {
		if (!a)
			throw Error();
		if (2 < arguments.length) {
			var d = Array.prototype.slice.call(arguments, 2);
			return function () {
				var c = Array.prototype.slice.call(arguments);
				Array.prototype.unshift.apply(c, d);
				return a.apply(b, c)
			}
		}
		return function () {
			return a.apply(b, arguments)
		}
	}
	function u(a, b, c) {
		Function.prototype.bind && -1 != Function.prototype.bind.toString().indexOf("native code") ? u = ba : u = ca;
		return u.apply(null, arguments)
	}
	var da = Date.now || function () {
		return +new Date
	};
	function v(a, b) {
		var c = a.split("."),
		d = k;
		c[0]in d || "undefined" == typeof d.execScript || d.execScript("var " + c[0]);
		for (var e; c.length && (e = c.shift()); )
			c.length || void 0 === b ? d[e] && d[e] !== Object.prototype[e] ? d = d[e] : d = d[e] = {}
		 : d[e] = b
	}
	function w(a, b) {
		function c() {}
		c.prototype = b.prototype;
		a.J = b.prototype;
		a.prototype = new c;
		a.prototype.constructor = a;
		a.L = function (a, c, g) {
			for (var d = Array(arguments.length - 2), e = 2; e < arguments.length; e++)
				d[e - 2] = arguments[e];
			return b.prototype[c].apply(a, d)
		}
	};
	var x = Array.prototype.indexOf ? function (a, b) {
		return Array.prototype.indexOf.call(a, b, void 0)
	}
	 : function (a, b) {
		if (l(a))
			return l(b) && 1 == b.length ? a.indexOf(b, 0) : -1;
		for (var c = 0; c < a.length; c++)
			if (c in a && a[c] === b)
				return c;
		return -1
	},
	y = Array.prototype.forEach ? function (a, b, c) {
		Array.prototype.forEach.call(a, b, c)
	}
	 : function (a, b, c) {
		for (var d = a.length, e = l(a) ? a.split("") : a, g = 0; g < d; g++)
			g in e && b.call(c, e[g], g, a)
	};
	function ea(a, b) {
		a: {
			var c = a.length;
			for (var d = l(a) ? a.split("") : a, e = 0; e < c; e++)
				if (e in d && b.call(void 0, d[e], e, a)) {
					c = e;
					break a
				}
			c = -1
		}
		return 0 > c ? null : l(a) ? a.charAt(c) : a[c]
	}
	function fa(a) {
		return Array.prototype.concat.apply([], arguments)
	}
	function z(a) {
		var b = a.length;
		if (0 < b) {
			for (var c = Array(b), d = 0; d < b; d++)
				c[d] = a[d];
			return c
		}
		return []
	};
	function A(a, b) {
		this.f = a;
		this.g = b;
		this.b = 0;
		this.a = null
	}
	A.prototype.get = function () {
		if (0 < this.b) {
			this.b--;
			var a = this.a;
			this.a = a.next;
			a.next = null
		} else
			a = this.f();
		return a
	};
	var ha = /&/g,
	ia = /</g,
	ja = />/g,
	ka = /"/g,
	la = /'/g,
	ma = /\x00/g,
	na = /[\x00&<>"']/;
	var B;
	a: {
		var C = k.navigator;
		if (C) {
			var D = C.userAgent;
			if (D) {
				B = D;
				break a
			}
		}
		B = ""
	};
	function oa(a) {
		var b = E,
		c;
		for (c in b)
			if (a.call(void 0, b[c], c, b))
				return c
	};
	function pa(a) {
		k.setTimeout(function () {
			throw a;
		}, 0)
	}
	var F;
	function qa() {
		var a = k.MessageChannel;
		"undefined" === typeof a && "undefined" !== typeof window && window.postMessage && window.addEventListener && -1 == B.indexOf("Presto") && (a = function () {
			var a = document.createElement("IFRAME");
			a.style.display = "none";
			a.src = "";
			document.documentElement.appendChild(a);
			var b = a.contentWindow;
			a = b.document;
			a.open();
			a.write("");
			a.close();
			var c = "callImmediate" + Math.random(),
			d = "file:" == b.location.protocol ? "*" : b.location.protocol + "//" + b.location.host;
			a = u(function (a) {
					if (("*" == d || a.origin == d) &&
						a.data == c)
						this.port1.onmessage()
				}, this);
			b.addEventListener("message", a, !1);
			this.port1 = {};
			this.port2 = {
				postMessage: function () {
					b.postMessage(c, d)
				}
			}
		});
		if ("undefined" !== typeof a && -1 == B.indexOf("Trident") && -1 == B.indexOf("MSIE")) {
			var b = new a,
			c = {},
			d = c;
			b.port1.onmessage = function () {
				if (void 0 !== c.next) {
					c = c.next;
					var a = c.F;
					c.F = null;
					a()
				}
			};
			return function (a) {
				d.next = {
					F: a
				};
				d = d.next;
				b.port2.postMessage(0)
			}
		}
		return "undefined" !== typeof document && "onreadystatechange" in document.createElement("SCRIPT") ? function (a) {
			var b = document.createElement("SCRIPT");
			b.onreadystatechange = function () {
				b.onreadystatechange = null;
				b.parentNode.removeChild(b);
				b = null;
				a();
				a = null
			};
			document.documentElement.appendChild(b)
		}
		 : function (a) {
			k.setTimeout(a, 0)
		}
	};
	function G() {
		this.b = this.a = null
	}
	var I = new A(function () {
			return new H
		}, function (a) {
			a.reset()
		});
	G.prototype.add = function (a, b) {
		var c = I.get();
		c.set(a, b);
		this.b ? this.b.next = c : this.a = c;
		this.b = c
	};
	G.prototype.remove = function () {
		var a = null;
		this.a && (a = this.a, this.a = this.a.next, this.a || (this.b = null), a.next = null);
		return a
	};
	function H() {
		this.next = this.b = this.a = null
	}
	H.prototype.set = function (a, b) {
		this.a = a;
		this.b = b;
		this.next = null
	};
	H.prototype.reset = function () {
		this.next = this.b = this.a = null
	};
	function ra(a) {
		J || sa();
		K || (J(), K = !0);
		ta.add(a, void 0)
	}
	var J;
	function sa() {
		if (k.Promise && k.Promise.resolve) {
			var a = k.Promise.resolve(void 0);
			J = function () {
				a.then(ua)
			}
		} else
			J = function () {
				var a = ua,
				c;
				!(c = "function" != p(k.setImmediate)) && (c = k.Window && k.Window.prototype) && (c = -1 == B.indexOf("Edge") && k.Window.prototype.setImmediate == k.setImmediate);
				c ? (F || (F = qa()), F(a)) : k.setImmediate(a)
			}
	}
	var K = !1,
	ta = new G;
	function ua() {
		for (var a; a = ta.remove(); ) {
			try {
				a.a.call(a.b)
			} catch (c) {
				pa(c)
			}
			var b = I;
			b.g(a);
			100 > b.b && (b.b++, a.next = b.a, b.a = a)
		}
		K = !1
	};
	function L() {
		this.f = this.f;
		this.g = this.g
	}
	L.prototype.f = !1;
	L.prototype.dispose = function () {
		this.f || (this.f = !0, this.A())
	};
	L.prototype.A = function () {
		if (this.g)
			for (; this.g.length; )
				this.g.shift()()
	};
	function va(a, b) {
		var c,
		d;
		var e = document;
		e = b || e;
		if (e.querySelectorAll && e.querySelector && a)
			return e.querySelectorAll(a ? "." + a : "");
		if (a && e.getElementsByClassName) {
			var g = e.getElementsByClassName(a);
			return g
		}
		g = e.getElementsByTagName("*");
		if (a) {
			var f = {};
			for (c = d = 0; e = g[c]; c++) {
				var n = e.className,
				N;
				if (N = "function" == typeof n.split)
					N = 0 <= x(n.split(/\s+/), a);
				N && (f[d++] = e)
			}
			f.length = d;
			return f
		}
		return g
	}
	function wa(a, b) {
		for (var c = 0; a; ) {
			if (b(a))
				return a;
			a = a.parentNode;
			c++
		}
		return null
	};
	var xa = k.JSON.stringify;
	function M(a) {
		L.call(this);
		this.m = 1;
		this.h = [];
		this.i = 0;
		this.a = [];
		this.b = {};
		this.o = !!a
	}
	w(M, L);
	h = M.prototype;
	h.subscribe = function (a, b, c) {
		var d = this.b[a];
		d || (d = this.b[a] = []);
		var e = this.m;
		this.a[e] = a;
		this.a[e + 1] = b;
		this.a[e + 2] = c;
		this.m = e + 3;
		d.push(e);
		return e
	};
	function ya(a, b, c) {
		var d = O;
		if (a = d.b[a]) {
			var e = d.a;
			(a = ea(a, function (a) {
						return e[a + 1] == b && e[a + 2] == c
					})) && d.D(a)
		}
	}
	h.D = function (a) {
		var b = this.a[a];
		if (b) {
			var c = this.b[b];
			if (0 != this.i)
				this.h.push(a), this.a[a + 1] = aa;
			else {
				if (c) {
					var d = x(c, a);
					0 <= d && Array.prototype.splice.call(c, d, 1)
				}
				delete this.a[a];
				delete this.a[a + 1];
				delete this.a[a + 2]
			}
		}
		return !!b
	};
	h.H = function (a, b) {
		var c = this.b[a];
		if (c) {
			for (var d = Array(arguments.length - 1), e = 1, g = arguments.length; e < g; e++)
				d[e - 1] = arguments[e];
			if (this.o)
				for (e = 0; e < c.length; e++) {
					var f = c[e];
					za(this.a[f + 1], this.a[f + 2], d)
				}
			else {
				this.i++;
				try {
					for (e = 0, g = c.length; e < g; e++)
						f = c[e], this.a[f + 1].apply(this.a[f + 2], d)
				}
				finally {
					if (this.i--, 0 < this.h.length && 0 == this.i)
						for (; c = this.h.pop(); )
							this.D(c)
				}
			}
			return 0 != e
		}
		return !1
	};
	function za(a, b, c) {
		ra(function () {
			a.apply(b, c)
		})
	}
	h.clear = function (a) {
		if (a) {
			var b = this.b[a];
			b && (y(b, this.D, this), delete this.b[a])
		} else
			this.a.length = 0, this.b = {}
	};
	h.A = function () {
		M.J.A.call(this);
		this.clear();
		this.h.length = 0
	};
	var Aa = /^(?:([^:/?#.]+):)?(?:\/\/(?:([^/?#]*)@)?([^/#?]*?)(?::([0-9]+))?(?=[/#?]|$))?([^?#]+)?(?:\?([^#]*))?(?:#([\s\S]*))?$/;
	function Ba(a) {
		var b = a.match(Aa);
		a = b[1];
		var c = b[2],
		d = b[3];
		b = b[4];
		var e = "";
		a && (e += a + ":");
		d && (e += "//", c && (e += c + "@"), e += d, b && (e += ":" + b));
		return e
	}
	function Ca(a, b, c) {
		if ("array" == p(b))
			for (var d = 0; d < b.length; d++)
				Ca(a, String(b[d]), c);
		else
			null != b && c.push(a + ("" === b ? "" : "=" + encodeURIComponent(String(b))))
	}
	function Da(a) {
		var b = [],
		c;
		for (c in a)
			Ca(c, a[c], b);
		return b.join("&")
	}
	var Ea = /#|$/;
	var P = window.yt && window.yt.config_ || window.ytcfg && window.ytcfg.data_ || {};
	v("yt.config_", P);
	function Fa(a) {
		var b = arguments;
		if (1 < b.length)
			P[b[0]] = b[1];
		else {
			b = b[0];
			for (var c in b)
				P[c] = b[c]
		}
	};
	function Ga(a) {
		return a && window.yterr ? function () {
			try {
				return a.apply(this, arguments)
			} catch (b) {
				Ha(b)
			}
		}
		 : a
	}
	function Ha(a, b) {
		var c = m("yt.logging.errors.log");
		c ? c(a, b, void 0, void 0, void 0) : (c = [], c = "ERRORS" in P ? P.ERRORS : c, c.push([a, b, void 0, void 0, void 0]), Fa("ERRORS", c))
	};
	var Ia = 0;
	v("ytDomDomGetNextId", m("ytDomDomGetNextId") || function () {
		return ++Ia
	});
	var Ja = {
		stopImmediatePropagation: 1,
		stopPropagation: 1,
		preventMouseEvent: 1,
		preventManipulation: 1,
		preventDefault: 1,
		layerX: 1,
		layerY: 1,
		screenX: 1,
		screenY: 1,
		scale: 1,
		rotation: 1,
		webkitMovementX: 1,
		webkitMovementY: 1
	};
	function Q(a) {
		this.type = "";
		this.source = this.data = this.currentTarget = this.relatedTarget = this.target = null;
		this.charCode = this.keyCode = 0;
		this.metaKey = this.shiftKey = this.ctrlKey = this.altKey = !1;
		this.clientY = this.clientX = 0;
		this.changedTouches = this.touches = null;
		if (a = a || window.event) {
			this.a = a;
			for (var b in a)
				b in Ja || (this[b] = a[b]);
			(b = a.target || a.srcElement) && 3 == b.nodeType && (b = b.parentNode);
			this.target = b;
			if (b = a.relatedTarget)
				try {
					b = b.nodeName ? b : null
				} catch (c) {
					b = null
				}
			else
				"mouseover" == this.type ? b = a.fromElement :
					"mouseout" == this.type && (b = a.toElement);
			this.relatedTarget = b;
			this.clientX = void 0 != a.clientX ? a.clientX : a.pageX;
			this.clientY = void 0 != a.clientY ? a.clientY : a.pageY;
			this.keyCode = a.keyCode ? a.keyCode : a.which;
			this.charCode = a.charCode || ("keypress" == this.type ? this.keyCode : 0);
			this.altKey = a.altKey;
			this.ctrlKey = a.ctrlKey;
			this.shiftKey = a.shiftKey;
			this.metaKey = a.metaKey
		}
	}
	Q.prototype.preventDefault = function () {
		this.a && (this.a.returnValue = !1, this.a.preventDefault && this.a.preventDefault())
	};
	Q.prototype.stopPropagation = function () {
		this.a && (this.a.cancelBubble = !0, this.a.stopPropagation && this.a.stopPropagation())
	};
	Q.prototype.stopImmediatePropagation = function () {
		this.a && (this.a.cancelBubble = !0, this.a.stopImmediatePropagation && this.a.stopImmediatePropagation())
	};
	var E = m("ytEventsEventsListeners") || {};
	v("ytEventsEventsListeners", E);
	var Ka = m("ytEventsEventsCounter") || {
		count: 0
	};
	v("ytEventsEventsCounter", Ka);
	function La(a, b, c, d) {
		d = void 0 === d ? {}
		 : d;
		a.addEventListener && ("mouseenter" != b || "onmouseenter" in document ? "mouseleave" != b || "onmouseenter" in document ? "mousewheel" == b && "MozBoxSizing" in document.documentElement.style && (b = "MozMousePixelScroll") : b = "mouseout" : b = "mouseover");
		return oa(function (e) {
			var g = "boolean" == typeof e[4] && e[4] == !!d,
			f;
			if (f = q(e[4]) && q(d))
				a: {
					f = e[4];
					for (var n in f)
						if (!(n in d) || f[n] !== d[n]) {
							f = !1;
							break a
						}
					for (n in d)
						if (!(n in f)) {
							f = !1;
							break a
						}
					f = !0
				}
			return !!e.length && e[0] == a && e[1] == b && e[2] == c &&
			(g || f)
		})
	}
	function Ma(a) {
		a && ("string" == typeof a && (a = [a]), y(a, function (a) {
				if (a in E) {
					var b = E[a],
					d = b[0],
					e = b[1],
					g = b[3];
					b = b[4];
					d.removeEventListener ? Na() || "boolean" == typeof b ? d.removeEventListener(e, g, b) : d.removeEventListener(e, g, !!b.capture) : d.detachEvent && d.detachEvent("on" + e, g);
					delete E[a]
				}
			}))
	}
	var Na = function (a) {
		var b = !1,
		c;
		return function () {
			b || (c = a(), b = !0);
			return c
		}
	}
	(function () {
		var a = !1;
		try {
			var b = Object.defineProperty({}, "capture", {
					get: function () {
						a = !0
					}
				});
			window.addEventListener("test", null, b)
		} catch (c) {}
		return a
	});
	function Oa(a, b, c) {
		var d = void 0 === d ? {}
		 : d;
		if (a && (a.addEventListener || a.attachEvent)) {
			var e = La(a, b, c, d);
			if (!e) {
				e = ++Ka.count + "";
				var g = !("mouseenter" != b && "mouseleave" != b || !a.addEventListener || "onmouseenter" in document);
				var f = g ? function (d) {
					d = new Q(d);
					if (!wa(d.relatedTarget, function (b) {
							return b == a
						}))
						return d.currentTarget = a, d.type = b, c.call(a, d)
				}
				 : function (b) {
					b = new Q(b);
					b.currentTarget = a;
					return c.call(a, b)
				};
				f = Ga(f);
				a.addEventListener ? ("mouseenter" == b && g ? b = "mouseover" : "mouseleave" == b && g ? b = "mouseout" : "mousewheel" == b && "MozBoxSizing" in document.documentElement.style && (b = "MozMousePixelScroll"), Na() || "boolean" == typeof d ? a.addEventListener(b, f, d) : a.addEventListener(b, f, !!d.capture)) : a.attachEvent("on" + b, f);
				E[e] = [a, b, c, f, d]
			}
		}
	};
	function Pa(a) {
		"function" == p(a) && (a = Ga(a));
		return window.setInterval(a, 250)
	};
	var Qa = {};
	function Ra(a) {
		return Qa[a] || (Qa[a] = String(a).replace(/\-([a-z])/g, function (a, c) {
					return c.toUpperCase()
				}))
	};
	var R = {},
	S = [],
	O = new M,
	Sa = {};
	function Ta() {
		y(S, function (a) {
			a()
		})
	}
	function Ua(a, b) {
		b || (b = document);
		var c = z(b.getElementsByTagName("yt:" + a)),
		d = "yt-" + a,
		e = b || document;
		d = z(e.querySelectorAll && e.querySelector ? e.querySelectorAll("." + d) : va(d, b));
		return fa(c, d)
	}
	function T(a, b) {
		var c;
		"yt:" == a.tagName.toLowerCase().substr(0, 3) ? c = a.getAttribute(b) : c = a ? a.dataset ? a.dataset[Ra(b)] : a.getAttribute("data-" + b) : null;
		return c
	}
	function Va(a, b) {
		O.H.apply(O, arguments)
	};
	function Wa(a) {
		this.b = a || {};
		this.g = {};
		this.f = this.a = !1;
		a = document.getElementById("www-widgetapi-script");
		if (this.a = !!("https:" == document.location.protocol || a && 0 == a.src.indexOf("https:"))) {
			a = [this.b, window.YTConfig || {}, this.g];
			for (var b = 0; b < a.length; b++)
				a[b].host && (a[b].host = a[b].host.replace("http://", "https://"))
		}
	}
	var U = null;
	function V(a, b) {
		for (var c = [a.b, window.YTConfig || {}, a.g], d = 0; d < c.length; d++) {
			var e = c[d][b];
			if (void 0 != e)
				return e
		}
		return null
	}
	function Xa(a, b, c) {
		U || (U = {}, Oa(window, "message", u(a.h, a)));
		U[c] = b
	}
	Wa.prototype.h = function (a) {
		if (a.origin == V(this, "host") || a.origin == V(this, "host").replace(/^http:/, "https:")) {
			try {
				var b = JSON.parse(a.data)
			} catch (c) {
				return
			}
			this.f = !0;
			this.a || 0 != a.origin.indexOf("https:") || (this.a = !0);
			if (a = U[b.id])
				a.B = !0, a.B && (y(a.u, a.C, a), a.u.length = 0), a.I(b)
		}
	};
	function W(a, b, c) {
		this.i = this.a = this.b = null;
		this.h = this[r] || (this[r] = ++t);
		this.f = 0;
		this.B = !1;
		this.u = [];
		this.g = null;
		this.m = c;
		this.o = {};
		c = document;
		if (a = l(a) ? c.getElementById(a) : a)
			if (c = "iframe" == a.tagName.toLowerCase(), b.host || (b.host = c ? Ba(a.src) : "https://www.youtube.com"), this.b = new Wa(b), c || (b = Ya(this, a), this.i = a, (c = a.parentNode) && c.replaceChild(b, a), a = b), this.a = a, this.a.id || (a = b = this.a, a = a[r] || (a[r] = ++t), b.id = "widget" + a), R[this.a.id] = this, window.postMessage) {
				this.g = new M;
				Za(this);
				b = V(this.b, "events");
				for (var d in b)
					b.hasOwnProperty(d) && this.addEventListener(d, b[d]);
				for (var e in Sa)
					$a(this, e)
			}
	}
	h = W.prototype;
	h.setSize = function (a, b) {
		this.a.width = a;
		this.a.height = b;
		return this
	};
	h.K = function () {
		return this.a
	};
	h.I = function (a) {
		this.s(a.event, a)
	};
	h.addEventListener = function (a, b) {
		var c = b;
		"string" == typeof b && (c = function () {
			window[b].apply(window, arguments)
		});
		if (!c)
			return this;
		this.g.subscribe(a, c);
		ab(this, a);
		return this
	};
	function $a(a, b) {
		var c = b.split(".");
		if (2 == c.length) {
			var d = c[1];
			a.m == c[0] && ab(a, d)
		}
	}
	h.destroy = function () {
		this.a.id && (R[this.a.id] = null);
		var a = this.g;
		a && "function" == typeof a.dispose && a.dispose();
		if (this.i) {
			a = this.a;
			var b = a.parentNode;
			b && b.replaceChild(this.i, a)
		} else (a = this.a) && a.parentNode && a.parentNode.removeChild(a);
		U && (U[this.h] = null);
		this.b = null;
		a = this.a;
		for (var c in E)
			E[c][0] == a && Ma(c);
		this.i = this.a = null
	};
	h.v = function () {
		return {}
	};
	function X(a, b, c) {
		c = c || [];
		c = Array.prototype.slice.call(c);
		b = {
			event: "command",
			func: b,
			args: c
		};
		a.B ? a.C(b) : a.u.push(b)
	}
	h.s = function (a, b) {
		if (!this.g.f) {
			var c = {
				target: this,
				data: b
			};
			this.g.H(a, c);
			Va(this.m + "." + a, c)
		}
	};
	function Ya(a, b) {
		for (var c = document.createElement("iframe"), d = b.attributes, e = 0, g = d.length; e < g; e++) {
			var f = d[e].value;
			null != f && "" != f && "null" != f && c.setAttribute(d[e].name, f)
		}
		c.setAttribute("frameBorder", 0);
		c.setAttribute("allowfullscreen", 1);
		c.setAttribute("allow", "accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture");
		c.setAttribute("title", "YouTube " + V(a.b, "title"));
		(d = V(a.b, "width")) && c.setAttribute("width", d);
		(d = V(a.b, "height")) && c.setAttribute("height", d);
		var n = a.v();
		n.enablejsapi =
			window.postMessage ? 1 : 0;
		window.location.host && (n.origin = window.location.protocol + "//" + window.location.host);
		n.widgetid = a.h;
		window.location.href && y(["debugjs", "debugcss"], function (a) {
			var b = window.location.href;
			var c = b.search(Ea);
			b: {
				var d = 0;
				for (var e = a.length; 0 <= (d = b.indexOf(a, d)) && d < c; ) {
					var f = b.charCodeAt(d - 1);
					if (38 == f || 63 == f)
						if (f = b.charCodeAt(d + e), !f || 61 == f || 38 == f || 35 == f)
							break b;
					d += e + 1
				}
				d = -1
			}
			if (0 > d)
				b = null;
			else {
				e = b.indexOf("&", d);
				if (0 > e || e > c)
					e = c;
				d += a.length + 1;
				b = decodeURIComponent(b.substr(d, e - d).replace(/\+/g,
							" "))
			}
			null === b || (n[a] = b)
		});
		c.src = V(a.b, "host") + a.w() + "?" + Da(n);
		return c
	}
	h.G = function () {
		this.a && this.a.contentWindow ? this.C({
			event: "listening"
		}) : window.clearInterval(this.f)
	};
	function Za(a) {
		Xa(a.b, a, a.h);
		a.f = Pa(u(a.G, a));
		Oa(a.a, "load", u(function () {
				window.clearInterval(this.f);
				this.f = Pa(u(this.G, this))
			}, a))
	}
	function ab(a, b) {
		a.o[b] || (a.o[b] = !0, X(a, "addEventListener", [b]))
	}
	h.C = function (a) {
		a.id = this.h;
		a.channel = "widget";
		a = xa(a);
		var b = this.b;
		var c = Ba(this.a.src);
		b = 0 == c.indexOf("https:") ? [c] : b.a ? [c.replace("http:", "https:")] : b.f ? [c] : [c, c.replace("http:", "https:")];
		if (!this.a.contentWindow)
			throw Error("The YouTube player is not attached to the DOM.");
		for (c = 0; c < b.length; c++)
			try {
				this.a.contentWindow.postMessage(a, b[c])
			} catch (d) {
				if (d.name && "SyntaxError" == d.name)
					Ha(d, "WARNING");
				else
					throw d;
			}
	};
	function bb(a) {
		return (0 == a.search("cue") || 0 == a.search("load")) && "loadModule" != a
	}
	function cb(a) {
		return 0 == a.search("get") || 0 == a.search("is")
	};
	function Y(a, b) {
		if (!a)
			throw Error("YouTube player element ID required.");
		var c = {
			title: "video player",
			videoId: "",
			width: 640,
			height: 360
		};
		if (b)
			for (var d in b)
				c[d] = b[d];
		W.call(this, a, c, "player");
		this.j = {};
		this.l = {}
	}
	w(Y, W);
	function db(a) {
		if ("iframe" != a.tagName.toLowerCase()) {
			var b = T(a, "videoid");
			b && (b = {
					videoId: b,
					width: T(a, "width"),
					height: T(a, "height")
				}, new Y(a, b))
		}
	}
	h = Y.prototype;
	h.w = function () {
		return "/embed/" + V(this.b, "videoId")
	};
	h.v = function () {
		var a = V(this.b, "playerVars");
		if (a) {
			var b = {},
			c;
			for (c in a)
				b[c] = a[c];
			a = b
		} else
			a = {};
		window != window.top && document.referrer && (a.widget_referrer = document.referrer.substring(0, 256));
		if (c = V(this.b, "embedConfig")) {
			if (q(c))
				try {
					c = xa(c)
				} catch (d) {
					console.error("Invalid embed config JSON", d)
				}
			a.embed_config = c
		}
		return a
	};
	h.I = function (a) {
		var b = a.event;
		a = a.info;
		switch (b) {
		case "apiInfoDelivery":
			if (q(a))
				for (var c in a)
					this.j[c] = a[c];
			break;
		case "infoDelivery":
			eb(this, a);
			break;
		case "initialDelivery":
			window.clearInterval(this.f);
			this.l = {};
			this.j = {};
			fb(this, a.apiInterface);
			eb(this, a);
			break;
		default:
			this.s(b, a)
		}
	};
	function eb(a, b) {
		if (q(b))
			for (var c in b)
				a.l[c] = b[c]
	}
	function fb(a, b) {
		y(b, function (a) {
			this[a] || ("getCurrentTime" == a ? this[a] = function () {
				var a = this.l.currentTime;
				if (1 == this.l.playerState) {
					var b = (da() / 1E3 - this.l.currentTimeLastUpdated_) * this.l.playbackRate;
					0 < b && (a += Math.min(b, 1))
				}
				return a
			}
				 : bb(a) ? this[a] = function () {
				this.l = {};
				this.j = {};
				X(this, a, arguments);
				return this
			}
				 : cb(a) ? this[a] = function () {
				var b = 0;
				0 == a.search("get") ? b = 3 : 0 == a.search("is") && (b = 2);
				return this.l[a.charAt(b).toLowerCase() + a.substr(b + 1)]
			}
				 : this[a] = function () {
				X(this, a, arguments);
				return this
			})
		}, a)
	}
	h.getVideoEmbedCode = function () {
		var a = parseInt(V(this.b, "width"), 10);
		var b = parseInt(V(this.b, "height"), 10),
		c = V(this.b, "host") + this.w();
		na.test(c) && (-1 != c.indexOf("&") && (c = c.replace(ha, "&amp;")), -1 != c.indexOf("<") && (c = c.replace(ia, "&lt;")), -1 != c.indexOf(">") && (c = c.replace(ja, "&gt;")), -1 != c.indexOf('"') && (c = c.replace(ka, "&quot;")), -1 != c.indexOf("'") && (c = c.replace(la, "&#39;")), -1 != c.indexOf("\x00") && (c = c.replace(ma, "&#0;")));
		a = '<iframe width="' + a + '" height="' + b + '" src="' + c + '" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';
		return a
	};
	h.getOptions = function (a) {
		return this.j.namespaces ? a ? this.j[a].options || [] : this.j.namespaces || [] : []
	};
	h.getOption = function (a, b) {
		if (this.j.namespaces && a && b)
			return this.j[a][b]
	};
	function Z(a, b) {
		var c = {
			title: "Thumbnail",
			videoId: "",
			width: 120,
			height: 68
		};
		if (b)
			for (var d in b)
				c[d] = b[d];
		W.call(this, a, c, "thumbnail")
	}
	w(Z, W);
	function gb(a) {
		if ("iframe" != a.tagName.toLowerCase()) {
			var b = T(a, "videoid");
			if (b) {
				b = {
					videoId: b,
					events: {}
				};
				b.width = T(a, "width");
				b.height = T(a, "height");
				b.thumbWidth = T(a, "thumb-width");
				b.thumbHeight = T(a, "thumb-height");
				b.thumbAlign = T(a, "thumb-align");
				var c = T(a, "onclick");
				c && (b.events.onClick = c);
				new Z(a, b)
			}
		}
	}
	Z.prototype.w = function () {
		return "/embed/" + V(this.b, "videoId")
	};
	Z.prototype.v = function () {
		return {
			player: 0,
			thumb_width: V(this.b, "thumbWidth"),
			thumb_height: V(this.b, "thumbHeight"),
			thumb_align: V(this.b, "thumbAlign")
		}
	};
	Z.prototype.s = function (a, b) {
		Z.J.s.call(this, a, b ? b.info : void 0)
	};
	v("YT.PlayerState.UNSTARTED", -1);
	v("YT.PlayerState.ENDED", 0);
	v("YT.PlayerState.PLAYING", 1);
	v("YT.PlayerState.PAUSED", 2);
	v("YT.PlayerState.BUFFERING", 3);
	v("YT.PlayerState.CUED", 5);
	v("YT.get", function (a) {
		return R[a]
	});
	v("YT.scan", Ta);
	v("YT.subscribe", function (a, b, c) {
		O.subscribe(a, b, c);
		Sa[a] = !0;
		for (var d in R)
			$a(R[d], a)
	});
	v("YT.unsubscribe", function (a, b, c) {
		ya(a, b, c)
	});
	v("YT.Player", Y);
	v("YT.Thumbnail", Z);
	W.prototype.destroy = W.prototype.destroy;
	W.prototype.setSize = W.prototype.setSize;
	W.prototype.getIframe = W.prototype.K;
	W.prototype.addEventListener = W.prototype.addEventListener;
	Y.prototype.getVideoEmbedCode = Y.prototype.getVideoEmbedCode;
	Y.prototype.getOptions = Y.prototype.getOptions;
	Y.prototype.getOption = Y.prototype.getOption;
	S.push(function (a) {
		a = Ua("player", a);
		y(a, db)
	});
	S.push(function () {
		var a = Ua("thumbnail");
		y(a, gb)
	});
	"undefined" != typeof YTConfig && YTConfig.parsetags && "onload" != YTConfig.parsetags || Ta();
	var hb = m("onYTReady");
	hb && hb();
	var ib = m("onYouTubeIframeAPIReady");
	ib && ib();
	var jb = m("onYouTubePlayerAPIReady");
	jb && jb();
}).call(this);
