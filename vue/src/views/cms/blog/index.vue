<template>
    <div class="blog">
        <div class="blog-guid">
            <sc-list :data="group" @change="changeCat"></sc-list>
        </div>
        <div class="blog-body">
            <div class="blog-head">
                <el-tabs v-model="param.types" @tab-change="changeTab">
                    <el-tab-pane label="推荐" :name="1" />
                    <el-tab-pane label="最新" :name="2" />
                    <el-tab-pane label="最热" :name="3" />
                </el-tabs>
            </div>
            <div class="blog-list">
                <div v-for="(item, index) in list" :key="index" class="blog-item" @click="clickItem(item)">
                    <el-image class="icon" :src="getImage(item)" fit="fill" />
                    <div class="blog-info">
                        <div class="blog-dd">
                            <div class="title">{{ item.title }}</div>
                            <div class="time">{{ this.$TOOL.dateTimeFormat(item.time) }}</div>
                        </div>
                        <div class="summary">{{ item.summary }}</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'cms_blog',
    components: {
    },
    data() {
        return {
            showGrouploading: false,
            group: [],
            list: [],
            param: {
                cat_id: '0',
                types: 1,
                key: "",
            }
        };
    },
    mounted() {
        this.loadCat();
        this.loadList();
    },
    methods: {
        async loadCat() {
            var res = await this.$API.cmsrescat.list.get();
            if (!res || res.code != 200) {
                return;
            }

            res.data.forEach(element => {
                this.group.push({ id: element.id, label: element.namec, count: element.qty });
            });
        },
        async loadList() {
            var res = await this.$API.cmsblog.list.get(this.param);
            if (!res || res.code != 200) {
                return;
            }

            this.list = res.data;
        },
        changeCat(item) {
            this.param.cat_id = item.id;
            this.loadList();
        },
        changeTab(tabName) {
            this.param.types = tabName;
            this.loadList();
        },
        clickItem(item) {
            if (!item) {
                return;
            }

            this.$router.push({
                path: '/cms/blog/view',
                query: { 'id': item.id }
            })
        },
        getImage(item) {
            var image = this.$SCM.get_avatar(item.image);
            return this.$CONFIG.SERVER_URL + image;
        }
    },
};
</script>

<style>
.blog {
    max-width: 960px;
    margin: 10px auto;
    background-color: #fff;
}

.blog-guid {
    float: left;
    width: 250px;
    height: 300px;
    padding-left: 10px;
}

.blog-body {
    width: 700px;
    min-height: 840px;
    margin-left: 260px;
    border-left: 1px dotted #ccc;
}

.blog-head {
    margin: 0 10px;
}

.blog-list {
    margin: 0 10px;
}

.blog-item {
    display: flex;
    justify-content: space-between;
    flex-direction: row;
    margin: 10px 5px;
}

.blog-item .icon {
    width: 64px;
    height: 64px;
}

.blog-info {
    display: flex;
    justify-content: space-between;
    flex-direction: column;
    flex-basis: 100%;
    margin-left: 10px;
}

.blog-dd {
    display: flex;
    justify-content: space-between;
    flex-direction: row;
}

.blog-dd .time {
    color: #999;
}

.blog-info .title {
    font-weight: bold;
    line-height: 24px;
    overflow: hidden;
}

.blog-info .summary {
    color: #999;
    flex-basis: 100%;
}
</style>

<style>
.el-tabs__header {
    margin: 0px;
}
</style>