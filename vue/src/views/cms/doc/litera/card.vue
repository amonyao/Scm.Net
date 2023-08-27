<template>
    <div class="page" :style="pageStyle">
        <el-card class="card" :style="cardStyle">
            <div class="card_head" :style="headStyle">
                {{ article.title }}
            </div>
            <div class="card_body" :style="bodyStyle">
                <div class="content" :style="contentStyle">
                    {{ article.content }}
                </div>
            </div>
            <div class="card_foot" :style="footStyle"></div>
            <div id="app_info" :style="appStyle"></div>
            <div id="cal_info" :style="calStyle"></div>
        </el-card>
    </div>
</template>

<script>
export default {
    data() {
        return {
            article: {},
            pageStyle: {},
            cardStyle: {},
            headStyle: {},
            bodyStyle: {},
            footStyle: {},
            appStyle: {},
            calStyle: {},
            contentStyle: {},
            style: {},
        }
    },
    mounted() {
        this.next();
    },
    methods: {
        async next() {
            var res = await this.$API.cmsdoclitera.view.get('1694902074548031488');
            if (!res || res.code != 200) {
                return;
            }

            this.article = res.data;
            var style = res.data.style;
            if (style) {
                this.style = eval('(' + style + ')');
            }
            this.parseStyle(this.style);
        },
        parseStyle(style) {
            if (!style) {
                return;
            }

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
                    obj.backgroundImage = "url('" + this.$CONFIG.SERVER_URL + image.path + "')";
                    console.log(obj.backgroundImage)
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
                obj.fontSize = style.font_size;
            }
        },
        parseSize(obj, style) {
            if (!style) {
                return;
            }

            if (style.width) {
                obj.width = style.width;
            }
            if (style.min_width) {
                obj.minWidth = style.min_width;
            }
            if (style.height) {
                obj.height = style.height;
            }
            if (style.min_height) {
                obj.minHeight = style.min_height;
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
                obj.left = style.left;
            }
            if (style.top) {
                obj.top = style.top;
            }
            if (style.right) {
                obj.right = style.right;
            }
            if (style.bottom) {
                obj.bottom = style.bottom;
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
                obj.marginLeft = margin.left;
            }
            if (margin.top) {
                obj.marginTop = margin.top;
            }
            if (margin.right) {
                obj.marginRight = margin.right;
            }
            if (margin.bottom) {
                obj.marginBottom = margin.bottom;
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
        }
    }
}
</script>
<style scoped>
.page {
    height: 100%;
}

.card {
    width: 600px;
    margin: 0 auto;
}

.card_head {
    display: none;
}

.card_body {
    display: table;
}

.content {
    display: table-cell;
}

.card_foot {
    display: none;
}

.app_info {
    position: relative;
}

.cal_info {
    position: relative;
}
</style>
