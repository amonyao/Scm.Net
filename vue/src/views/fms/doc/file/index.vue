<template>
    <div class="page">
        <scWaterfall :data="list" @preload="preload" @showContent="showContent">
            <template #item="{ item }">
                <img :data-src="item.src" :alt="item.names" :style="{ 'height': item._dim + 'px', }">
                <div>
                    <label>这是说明文字</label>
                </div>
            </template>
        </scWaterfall>
    </div>
</template>
<script>
export default {
    data() {
        return {
            apiObj: this.$API.cmsdocarticle.page,
            list: [],
            imgWidth: 100,
            param: {
                types_id: '0',
                row_status: 1,
                create_time: '',
                key: ''
            },
            selection: [],
            column: [
                { label: "id", prop: "id", hide: true },
                { prop: 'types', label: '类型', width: 100, formatter: this.getTypesNames },
                { prop: 'title', label: '主标题', width: 100 },
                { prop: 'qty', label: '点赞数量', width: 100 },
                { prop: 'fav_qty', label: '收藏数量', width: 100 },
                { prop: 'msg_qty', label: '留言数量', width: 100 },
                { prop: 'cat_id', label: '分类', width: 100 },
                { prop: 'author_names', label: '作者', width: 100 },
                { prop: 'row_status', label: '数据状态', width: 80 },
                { prop: 'update_names', label: '更新人员', width: 100 },
                { prop: 'update_time', label: '更新时间', width: "150", sortable: true, formatter: this.$TOOL.dateTimeFormat },
                { prop: 'create_names', label: '创建人员', width: 100 },
                { prop: 'create_time', label: '创建时间', width: "150", sortable: true, formatter: this.$TOOL.dateTimeFormat },
            ],
            row_status_list: [this.$SCM.OPTION_ALL],
            types_list: [this.$SCM.OPTION_ALL],
        };
    },
    mounted() {
        //this.$SCM.list_dic(this.types_list, 'article_type', true);
        this.list = this.testData();
        console.log("parent:" + this.list.length)
    },
    methods: {
        complete() {
            this.$refs.table.refresh();
        },
        testData() {
            var imgArr = [{ 'src': 'https://vitejs.cn/vite3-cn/logo-with-shadow.png', 'width': 595, 'height': 808 },
            { 'src': 'https://fscdn.zto.com/fs8/M02/94/F3/wKhBD19OTuuAedCpAAIKGsFFPqc560.png', 'width': 595, 'height': 808 },
            { 'src': 'https://vitejs.cn/vite3-cn/logo-with-shadow.png', 'width': 188, 'height': 121 },
            { 'src': 'https://vitejs.cn/vite3-cn/logo-with-shadow.png', 'width': 1317, 'height': 845 },
            { 'src': 'https://fscdn.zto.com/fs8/M02/94/F3/wKhBD19OTuuAedCpAAIKGsFFPqc560.png', 'width': 1349, 'height': 799 }];

            var list = [];
            for (let i = 0; i < 40; i++) {
                var idx = Math.round(Math.random() * (imgArr.length - 1));
                var img = imgArr[idx];
                //var tmp = this.imgWidth * img.height / img.width;
                img.height = (Math.random() * img.height) + 100;
                list.push(img);
            }

            return list;
        },
        preload(item) {
            // 指定大小
            if (item.height && item.width) {
                this.calSize(item, item.width, item.height, this.imgWidth);
                return;
            }

            // 无图则把高度设置为0
            var src = item.src;
            if (!src) {
                this.calSize(item, 0, 0, this.imgWidth);
                return;
            }

            let img = new Image();
            img.src = src;
            img.onload = img.onerror = (e) => {
                var width = 0;
                var height = 0;
                if (e.type === "error") {
                    item._error = true;
                }
                else if (e.type === 'load') {
                    width = img.width;
                    height = img.height;
                }

                this.calSize(item, width, height, this.imgWidth);
            }
        },
        // 明细展示
        showContent(item) {
            let img = item.children[0];
            if (img) {
                if (img.loaded) {
                    return;
                }

                img.src = img.getAttribute("data-src");
                img.style.opacity = 1;
                img.style.transform = "scale(1)";
                img.loaded = true;
            }
        },
        // 计算图片大小
        calSize(item, width, height, def) {
            var tmp = def;
            if (width && height) {
                tmp = Math.round(this.imgWidth * height / width);
            }
            item._dim = tmp;
            console.log('size:' + def)
            // ++this.loadedCount;
            // // 当前图片都与处理完，则加载图片
            // if (this.apiData.length === this.loadedCount) {
            //     this.preloaded();
            // }
        }
    }
}
</script>
<style scoped>
.page {
    width: 800px;
    height: 100%;
    margin: 0 auto;
    background-color: white;
}

.waterfall-item img {
    width: 100%;
    border-radius: 10px;
    opacity: 0;
    transform: scale(0.5);
    transition: all 0.6s;
    transition-delay: 0.1s;
}
</style>