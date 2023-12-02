<template>
    <div class="page" :style="pageStyle">
        <div class="card" :style="cardStyle">
            <div class="card_head" :style="headStyle" :class="{ hidden: !headStyle }"></div>
            <div class="card_body" :style="bodyStyle">
                <div class="article_title" v-if="article.types != 20">
                    {{ article.title }}
                </div>
                <div class="article_sub_title" v-if="article.sub_title">
                    {{ article.sub_title }}
                </div>
                <div class="article_content" :style="contentStyle">
                    {{ article.content }}
                </div>
                <div class="article_origin" :class="{ hidden: true }">
                    {{ article.origin }}
                </div>
            </div>
            <div class="card_foot" :style="footStyle" :class="{ hidden: !footStyle }"></div>
            <div id="app_info" class="app_info" :style="appStyle" :class="{ hidden: !appStyle }"></div>
            <div id="cal_info" class="cal_info" :style="calStyle" :class="{ hidden: !calStyle }"></div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            article: {},
            pageStyle: {},
            cardStyle: {},
            headStyle: null,
            bodyStyle: {},
            footStyle: null,
            appStyle: null,
            calStyle: null,
            contentStyle: {},
            style: {},
            scale: this.$TOOL.isMobile() ? 1 : 2
        }
    },
    mounted() {
    },
    methods: {
        showArticle(article) {
            this.article = article;
            var style = this.article.style;
            if (style) {
                this.style = eval('(' + style + ')');
            }
            this.parseStyle(this.style);
        },
        parseStyle(style) {
            if (!style) {
                return;
            }

            //this.scale = style.scale || 1;
            this.parsePageStyle(style.page_style);
            this.parseCardStyle(style.card_style);
            this.parseHeadStyle(style.head_style);
            this.parseBodyStyle(style.body_style);
            this.parseContentStyle(style.sections_style, style);
            this.parseFootStyle(style.foot_style);
            this.parseAppStyle(style.app_info_style);
            this.parseCalStyle(style.cal_solar_style);
        },
        parsePageStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.pageStyle, style);
            this.parseFont(this.pageStyle, style);
            this.parseSize(this.pageStyle, style);
            this.parseBounds(this.pageStyle, style);
            this.parseMargin(this.pageStyle, style);
            this.parseAlign(this.pageStyle, style);
        },
        parseCardStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.cardStyle, style);
            this.parseFont(this.cardStyle, style);
            this.parseSize(this.cardStyle, style);
            this.parseBounds(this.cardStyle, style);
            this.parseMargin(this.cardStyle, style);
            this.parseAlign(this.cardStyle, style);
        },
        parseHeadStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.headStyle, style);
            this.parseFont(this.headStyle, style);
            this.parseSize(this.headStyle, style);
            this.parseBounds(this.headStyle, style);
            this.parseMargin(this.headStyle, style);
            this.parseAlign(this.headStyle, style);
        },
        parseBodyStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.bodyStyle, style);
            this.parseFont(this.bodyStyle, style);
            this.parseSize(this.bodyStyle, style);
            this.parseBounds(this.bodyStyle, style);
            this.parseMargin(this.bodyStyle, style);
            this.parseAlign(this.bodyStyle, style);
        },
        parseContentStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.contentStyle, style);
            this.parseFont(this.contentStyle, style);
            this.parseSize(this.contentStyle, style);
            this.parseBounds(this.contentStyle, style);
            this.parseMargin(this.contentStyle, style);
            this.parseAlign(this.contentStyle, style);
        },
        parseFootStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.footStyle, style);
            this.parseFont(this.footStyle, style);
            this.parseSize(this.footStyle, style);
            this.parseBounds(this.footStyle, style);
            this.parseMargin(this.footStyle, style);
            this.parseAlign(this.footStyle, style);
        },
        parseAppStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.appStyle, style);
            this.parseFont(this.appStyle, style);
            this.parseSize(this.appStyle, style);
            this.parseBounds(this.appStyle, style);
            this.parseMargin(this.appStyle, style);
            this.parseAlign(this.appStyle, style);
        },
        parseCalStyle(style) {
            if (!style) {
                return;
            }

            this.parseBackground(this.calStyle, style);
            this.parseFont(this.calStyle, style);
            this.parseSize(this.calStyle, style);
            this.parseBounds(this.calStyle, style);
            this.parseMargin(this.calStyle, style);
            this.parseAlign(this.calStyle, style);
        },
        parseBackground(obj, style) {
            if (!style) {
                return;
            }

            if (style.background_color) {
                obj.backgroundColor = style.background_color;
            }
            if (style.background_image) {
                var image = style.background_image;
                if (image.path) {
                    var path = "url('" + this.$CONFIG.SERVER_URL + image.path + "')"
                    console.log(obj.backgroundImage)
                    obj.backgroundImage = path;
                }
                if (image.repeat) {
                    obj.backgroundRepeat = image.repeat;
                }
                if (image.position) {
                    obj.backgroundPosition = image.position;
                }
            }
        },
        parseFont(obj, style) {
            if (!style) {
                return;
            }

            if (style.font_name) {
                obj.fontName = style.font_name;
            }
            if (style.font_size) {
                obj.fontSize = this.getSize(style.font_size);
            }
        },
        parseSize(obj, style) {
            if (!style) {
                return;
            }

            if (style.width) {
                obj.width = this.getSize(style.width);
            }
            if (style.min_width) {
                obj.minWidth = this.getSize(style.min_width);
            }
            if (style.height) {
                obj.height = this.getSize(style.height);
            }
            if (style.min_height) {
                obj.minHeight = this.getSize(style.min_height);
            }
        },
        parseBounds(obj, style) {
            if (!style) {
                return;
            }

            if (style.position) {
                obj.position = style.position;
            }
            if (style.left) {
                obj.left = this.getSize(style.left);
            }
            if (style.top) {
                obj.top = this.getSize(style.top);
            }
            if (style.right) {
                obj.right = this.getSize(style.right);
            }
            if (style.bottom) {
                obj.bottom = this.getSize(style.bottom);
            }
        },
        parseMargin(obj, style) {
            if (!style) {
                return;
            }

            if (!style.margin) {
                return;
            }
            var margin = style.margin;
            if (margin.left) {
                obj.marginLeft = this.getSize(margin.left);
            }
            if (margin.top) {
                obj.marginTop = this.getSize(margin.top);
            }
            if (margin.right) {
                obj.marginRight = this.getSize(margin.right);
            }
            if (margin.bottom) {
                obj.marginBottom = this.getSize(margin.bottom);
            }
        },
        parseAlign(obj, style) {
            if (!style) {
                return;
            }

            if (style.halign) {
                obj.textAlign = style.halign;
            }
            if (style.valign) {
                obj.verticalAlign = style.valign;
            }
        },
        getSize(size) {
            if (!size) {
                return 0;
            }

            var reg = /\d+(\.\d+)?/
            var arr = reg.exec(size);
            var num = '0';
            var unit = '';
            if (arr.length > 1) {
                num = arr[0];
                unit = size.replace(num, '');
            }

            return num * this.scale + unit;
        }
    }
}
</script>
<style scoped>
.page {
    min-height: 100%;
    padding: 20px;
    background-size: cover;
}

.card {
    color: #333;
    width: 540px;
    min-height: 600px;
    margin: 0 auto;
    position: relative;
    font-family: '微软雅黑';
    font-size: 14pt;
    background-size: contain;
    border-radius: 4px;
    border: 1px solid #e4e7ed;
    overflow: hidden;
    transition: 0.3s;
    box-shadow: 0px 0px 12px rgba(0, 0, 0, .12);
}

.card_head {}

.card_body {
    position: relative;
    min-height: 400px;
}

.sections {}

.article_title {
    text-align: center;
    font-size: 20pt;
}

.article_sub_title {
    text-align: center;
    font-size: 16pt;
    color: #666;
}

.article_content {
    /*flex 布局*/
    display: flex;
    /*实现垂直居中*/
    align-items: center;
    /*实现水平居中*/
    justify-content: center;

    text-align: justify;
    width: 200px;
    height: 200px;
    margin: 0 auto;
}

.article_origin {
    text-align: right;
    bottom: 0px;
    color: #666;
}

.card_foot {
    position: absolute;
    bottom: 0px;
}

.app_info {
    position: absolute;
    width: 80px;
    height: 80px;
    overflow: hidden;
    bottom: 0;
    left: 0;
    background-color: #989898;
}

.cal_info {
    position: absolute;
    width: 80px;
    height: 80px;
    overflow: hidden;
    bottom: 0;
    right: 0;
    background-color: #989898;
}

.flow {}

.fixed {
    position: absolute;
}

.hidden {
    display: none;
}

@media (max-width: 1000px) {
    .card {
        width: 100%;
    }
}
</style>