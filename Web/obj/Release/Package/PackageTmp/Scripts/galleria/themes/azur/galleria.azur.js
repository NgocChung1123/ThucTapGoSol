/* Galleria Azur Theme 2012-08-09 | http://galleria.io/license/ | (c) Aino */
(function ($) {
    Galleria.addTheme({
        name:"azur",
        author:"Galleria",
        css:"galleria.azur.css",
        defaults:{
            transition:"fade",
            transitionSpeed:500,
            imageCrop:false,
            thumbCrop:"height",
            idleMode:"hover",
            idleSpeed:500,
            fullscreenTransition:false,
            trueFullscreen:false,
            _locale:{
                show_captions:"Hiện thông tin",
                hide_captions:"Ẩn thông tin",
                play:"Chạy slideshow",
                pause:"Dừng slideshow",
                enter_fullscreen:"Vào chế độ toàn màn hình",
                exit_fullscreen:"Kết thúc chế độ toàn màn hình",
                next:"File sau",
                prev:"File trước",
                showing_image:"Xem ảnh %s / %s",
				print:"In",
				download:"Tải về",
				closeX: "Đóng"
            },
            _toggleCaption:true,
            _showCaption:true,
            _showTooltip:true
        },
        init:function (options) {
            Galleria.requires(1.28, "This version of Azur theme requires Galleria version 1.2.8 or later"),
			this.addElement("bar", "fullscreen", "play", "print", "download", "progress", "closeX").append({
                stage:"progress",
                container:"bar",
                bar:["fullscreen", "play", "print", "download", "thumbnails-container"]
            }).prependChild("stage", "info").appendChild("container", "tooltip").appendChild("container", "closeX");

            var self = this,
                doc = window.document,
                e = options._locale,
                isCanvasPresent = "getContext" in doc.createElement("canvas");

            (function () {
                if (!isCanvasPresent) {
                    self.addElement("progressbar").appendChild("progress", "progressbar"), self.$("progress").addClass("nocanvas");
                    var b = self.$("progress").width();
                    self.bind("progress", function (a) {
                        self.$("progressbar").width(a.percent / 100 * b)
                    });
                    return
                }
                var e = 24,
                    g = doc.createElement("canvas"),
                    h = g.getContext("2d"),
                    i = function (a) {
                        return a * (Math.PI / 180)
                    }, j = function (a, b) {
                        h.strokeStyle = b || "#000", h.lineWidth = 3, h.clearRect(0, 0, e, e), h.beginPath(), h.arc(e / 2, e / 2, e / 2 - 2, i(-90), i(a - 90), !1), h.stroke(), h.closePath()
                    };
                g.width = e, g.height = e, $(g).css({
                    zIndex:1e4,
                    position:"absolute",
                    right:10,
                    top:10
                }).appendTo(self.get("container")), self.bind("progress", function (b) {
                    $(g).fadeIn(200), j(b.percent * 3.6, "rgba(255,255,255,.7)")
                }), self.bind("pause", function () {
                    $(g).fadeOut(200, function () {
                        h.clearRect(0, 0, e, e)
                    })
                })
            })(),
                function () {
                    if (!isCanvasPresent) {
                        self.$("loader").addClass("nocanvas");
                        return
                    }
                    var b = doc.createElement("canvas"),
                        e = b.getContext("2d"),
                        g = Math,
                        h = function (a, b, c) {
                            var d = c ? -2 : 2;
                            a.translate(b / d, b / d)
                        }, i = 28;
                    $(b).hide().appendTo(self.get("loader")).fadeIn(500);
                    var j = function (a, b) {
                        var c = 48,
                            d = 28,
                            e;
                        a.clearRect(0, 0, c, c), a.lineWidth = 1.5;
                        for (var f = 0; f < d; f++) e = f + b >= d ? f - d + b : f + b, a.strokeStyle = "rgba(255,255,255," + g.max(0, e / d) + ")", a.beginPath(), a.moveTo(c / 2, c / 2 - 16), a.lineTo(c / 2, 0), a.stroke(1), h(a, c, !1), a.rotate(360 / d * g.PI / 180), h(a, c, !0);
                        a.save(), h(a, c, !1), a.rotate(-1 * (360 / d / 8) * g.PI / 180), h(a, c, !0)
                    };
                    window.setInterval(function () {
                        j(e, i), i = i === 0 ? 28 : i - 1
                    }, 20)
                }();

            var g = Galleria.IE < 9 ? { bottom:-100 } : { bottom:-50, opacity:0},
				h = Galleria.IE < 9 ? { top:-20 } : { opacity:0, top:-20 };

            this.bind("play",function () {
                this.$("play").addClass("pause"), isCanvasPresent || this.$("progress").show()
            }).bind("pause",function () {
                    this.$("play").removeClass("pause"), isCanvasPresent || this.$("progress").hide()
			}).bind("loadstart",function (a) {
                    a.cached || this.$("loader").show()
			}).bind("loadfinish", function (a) {
                    isCanvasPresent ? this.$("loader").fadeOut(100) : this.$("loader").hide()
			}),

			this
				.addIdleState(this.get("info"), g, Galleria.IE < 9 ? {} : { opacity:1 }, !0)
				.addIdleState(this.get("image-nav-left"), { opacity:0, left:0 }, { opacity:1 }, !0)
				.addIdleState(this.get("image-nav-right"), { opacity:0, right:0 }, { opacity:1 }, !0)
				.addIdleState(this.get("counter"), h, Galleria.IE < 9 ? {} : { opacity:.9 }, !0),

			this.$("fullscreen").click(function (a) {
//                a.preventDefault();
//				c.toggleFullscreen();
                var original_div = self._target;
                self.destroy();
                jQuery(original_div).css("display","none");
				jQuery(original_div).data('galleria',"");
				isAtGalleriaFullscreen = false;					// for showing close edit box confirm dialog
//                jQuery(original_div).html("");
//                c.toggleFullscreen();
            }),

			this.$("closeX").click(function (a) {
                var original_div = self._target;
                self.destroy();
                jQuery(original_div).css("display","none");
				jQuery(original_div).data('galleria',"");
				isAtGalleriaFullscreen = false;					// for showing close edit box confirm dialog
            }),

			this.$("play").click(function (a) {
                a.preventDefault(), self.playToggle()
            }),

			this.$("download").click(function (a) {
				a.preventDefault(),
				window.location.href = self.$("download").data("link");
			}),

			this.$("print").click(function (a) {
				a.preventDefault();
				var base_url = window.location.protocol + "//" + window.location.hostname +	(window.location.port && ":" + window.location.port);
				var link = base_url + self.$("print").data("link");
				var newtab = window.open();
				newtab.location = link;
				if (window.focus) { newtab.focus(); }
//				window.location.href = self.$("print").data("link");
			}),

			options._toggleCaption && (
				this.$("info").addClass("toggler"),
				this.addElement("captionopen").appendChild("stage", "captionopen"),
				this.addElement("captionclose").appendChild("info", "captionclose"),
				this.$("captionopen").click(function () {
                	self.$("info").addClass("open"), $(this).hide()
            	}).html(e.show_captions),

				this.bind("loadstart", function () {
                	this.$("captionopen").toggle(!self.$("info").hasClass("open") && this.hasInfo())
            	}),

				this.$("captionclose").click(function () {
                	self.$("info").removeClass("open"), self.hasInfo() && self.$("captionopen").show()
            	}).html("&#215;"),

				options._showCaption && this.$("captionopen").click()
			),

			options._showTooltip && this.bindTooltip({
                fullscreen:self.isFullscreen() ? e.exit_fullscreen : e.enter_fullscreen,
                play:function () {
                    return self.isPlaying() ? e.pause : e.play
                },
                captionclose:e.hide_captions,
                "image-nav-right":e.next,
                "image-nav-left":e.prev,
				"print": e.print,
				"download": e.download,
				"closeX": e.closeX,
                counter:function () {
                    return e.showing_image.replace(/\%s/, self.getIndex() + 1).replace(/\%s/, self.getDataLength())
                }
            })
        }
    })
})(jQuery);