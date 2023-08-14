<template>
    <div id="waterfall" ref="waterfall" @scroll="handleScroll">
        <div class="img-box default-card-animation" v-for="(image, index) in imageData" :key="index" ref="imgBox"
            :style="{ 'top': image.top + 'px', 'left': image.left + 'px', 'width': imgWidth + 'px', 'height': image._height + 'px', 'padding': imgGap + 'px' }">
            <img :data-src="image.src" :alt="image.names">
        </div>
    </div>
</template>
<script>
export default {
    props: {
        apiObj: { type: Object, default: () => { } },
        images: { type: Object, default: () => { } },
        columnCount: { type: Number, default: 5 },//列数量
        columnGap: { type: Number, default: 30 },//列间隙
        backgroundColor: { type: String, default: "rgba(128,128,128,0.2)" }
    },
    computed: {
        // isMobile() {
        //     return this.isMobiles();
        // },
        // // 容器 waterfall 的宽度
        viewWidth() {
            return this.$refs.waterfall.clientWidth;
        },
        viewHeight() {
            return this.$refs.waterfall.clientHeight;
        },
        // // 图片宽度
        // imgWidth() {
        //     return this.colWidth - 2 * this.colGap;
        // },
        // // 列宽度
        // colWidth() {
        //     return this.waterfallWidth / this.colCount;
        // },
        // // 列数
        // colNum() {
        //     return this.isMobile ? 2 : this.colCount;
        // }
    },
    data() {
        return {
            imageList: [],// 返回数据
            imageData: [],// 图像列表
            beginIndex: 0,
            loadedCount: 0,
            colGap: 10,// 列间隙
            imgGap: 0,//图像间隙
            colCount: 5,// 列数量
            colHeight: [],// 列高度
            colWidth: 0,
            imgWidth: 0,
            reachBottomDistance: 20, // 滚动触底距离，触发加载新图片
        }
    },
    mounted() {
        //this.scroll();
        this.init_data();
    },
    methods: {
        init_data() {
            // 初始化列宽度
            var dom = this.$refs.waterfall;
            let domWidth = dom.clientWidth;
            this.viewHeight = dom.clientHeight;
            console.log('viewheight:' + this.viewHeight)

            if (!this.colWidth && this.colCount) {
                var gap = this.colGap * (this.colCount - 1);
                this.colWidth = (domWidth - gap) / this.colCount;
            } else if (this.colWidth && !this.colCount) {
                this.colCount = Math.floor(domWidth / (this.colWidth + this.colGap))
            }

            if (this.colWidth < 200) {
                this.colWidth = 200;
                this.colCount = Math.floor(domWidth / (this.colWidth + this.colGap))
            }

            this.imgWidth = this.colWidth - this.colGap * 2;

            // 初始化列高度
            for (var i = 0; i < this.colCount; i += 1) {
                this.colHeight[i] = 0;
            }

            this.load_img();
        },
        async load_img() {
            var imgArr = [{ 'src': require('../../assets/01.png'), 'width': 595, 'height': 808 },
            { 'src': require('../../assets/02.png'), 'width': 595, 'height': 808 },
            { 'src': require('../../assets/03.png'), 'width': 188, 'height': 121 },
            { 'src': require('../../assets/04.png'), 'width': 1317, 'height': 845 },
            { 'src': require('../../assets/05.png'), 'width': 1349, 'height': 799 }];
            for (let i = 0; i < 40; i++) {
                var idx = Math.round(Math.random() * (this.colCount - 1));
                var img = imgArr[idx];
                var tmp = this.imgWidth * img.height / img.width;
                img.height = tmp;
                this.imageList.push(img);
            }

            this.preLoad();
        },
        // 预加载 设置并保存图片宽高
        preLoad() {
            // forEach 无法通过 item 直接修改数组元素，需用数组下标修改
            this.imageList.forEach((item, index) => {
                if (!item) {
                    return;
                }

                if (index < this.loadedCount) {
                    return;
                }

                // 无图则把高度设置为0
                if (!item.src) {
                    // 图片的高度
                    this.imageList[index]._height = "0";
                    ++this.loadedCount;
                    // 当前图片都与处理完，则加载图片
                    if (this.imageList.length === this.loadedCount) {
                        this.preloaded();
                    }
                    return;
                }

                let img = new Image();
                img.src = item.src;
                img.onload = img.onerror = (e) => {
                    // 若加载失败则设置图片高度与宽度一致，加载成功则动态计算图片高度
                    this.imageList[index]._height = this.imgWidth;
                    if (e.type === 'load') {
                        this.imageList[index]._height = Math.round(this.imgWidth * img.height / img.width);
                    }
                    else if (e.type === "error") {
                        this.imageList[index]._error = true;
                    }
                    ++this.loadedCount;
                    // 当前图片都与处理完，则加载图片
                    if (this.imageList.length === this.loadedCount) {
                        this.preloaded();
                    }
                }
            })
        },
        preloaded() {
            // 开始渲染
            this.imageData = [].concat(this.imageList);
            this.$nextTick(() => {
                // 对每个元素进行排列
                this.waterfall();
            });
        },
        // waterfall，等到整个视图都渲染完毕再执行
        waterfall() {
            // 选择所有图片
            this.imgBoxEls = this.$refs["imgBox"];
            // 若没图片，则返回
            if (!this.imgBoxEls)
                return;

            let top, left, height;
            for (let i = this.beginIndex; i < this.imgBoxEls.length; ++i) {
                var imgBox = this.imgBoxEls[i];
                if (!imgBox)
                    return;

                // 当前图片的高度
                height = imgBox.offsetHeight + this.colGap;

                // 找到当前最低的高度和其索引
                let minIdx = this.getColumn();
                let minHeight = this.colHeight[minIdx];
                // 当前图片的 top，即当前图片应所在的高度
                top = minHeight;
                // 当前图片的 left，即当前图片应该排到目前高度最低那一列下面
                left = minIdx * this.colWidth;
                // 更新第 minIdx 列的高度
                this.colHeight[minIdx] += height;

                // 设置 img-box 位置
                imgBox.style.top = top + "px";
                imgBox.style.left = left + "px";
                // 当前图片在窗口内，则加载，这是用于后面的图片懒加载。viewHeight 为窗口高度
                if (top < this.viewHeight) {
                    let imgEl = imgBox.children[0];
                    imgEl.src = imgEl.getAttribute("data-src");
                    imgEl.style.opacity = 1;
                    imgEl.style.transform = "scale(1)";
                }
            }
            // 排列完之后，之后新增图片从这个索引开始预加载图片和排列，之前排列的图片无需在处理
            this.beginIndex = this.imgBoxEls.length;
        },
        getColumn() {
            var min = this.colHeight[0];
            var idx = 0;
            for (var i = 1; i < this.colHeight.length; i += 1) {
                if (this.colHeight[i] < min) {
                    idx = i;
                    min = this.colHeight[i];
                }
            }
            return idx;
        },
        reset() {
            this.imageList = [];
            this.loadedCount = 0;
            this.beginIndex = 0;
        },
        // 滚动触底事件
        handleScroll() {
            let minHeight = Math.min.apply(null, this.colsHeightArr);
            // 滚动条滚动的高度
            let scrollTop = this.$refs.waterfall.scrollTop
            // 到达最底层的高度最低的一列，则触发 handleReachBottom 方法
            if (scrollTop + this.viewHeight > minHeight - this.reachBottomDistance) {
                this.load_img();
            }

            // 图片懒加载
            this.imgBoxEls.forEach((imgBoxEl) => {
                let imgEl = imgBoxEl.children[0];
                // 若已加载，则跳过
                if (imgEl.src)
                    return;
                // 当前图片所处的高度
                let top = imgBoxEl.style.top;
                top = Number.parseFloat(top.slice(0, top.length - 2));
                // 图片已到达可视范围，则加载
                if (scrollTop + this.viewHeight > top) {
                    imgEl.src = imgEl.getAttribute("data-src")
                    imgEl.style.opacity = 1;
                    imgEl.style.transform = "scale(1)";
                }
            })
        },
    }
}
</script>

<style scoped lang="scss">
#waterfall {
    width: 100%;
    height: 100%;
    position: relative;
    overflow: scroll;

    @keyframes show-card {
        0% {
            transform: scale(0.5);
        }

        100% {
            transform: scale(1);
        }
    }

    .img-box {
        position: absolute;
        border-radius: 10px;
        padding: 5px;
        padding-left: 0;
        border: 1px solid #eaeaea;
        background-color: #ffffff;

        img {
            width: 100%;
            border-radius: 10px;
            opacity: 0;
            transform: scale(0.5);
            transition: all 0.6s;
            transition-delay: 0.1s;
        }
    }

    .default-card-animation {
        animation: show-card 0.4s;
        transition: left 0.6s top 0.6s;
        transition-delay: 0.1s;
    }

}
</style>