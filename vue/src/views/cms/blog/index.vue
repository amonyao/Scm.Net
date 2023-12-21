<template>
    <div class="blog">
        <div class="blog-guid">
            <sc-list :data="group" :hideDo="true"></sc-list>
        </div>
        <div class="blog-body">
            <div class="blog-head">
                <el-tabs v-model="param.types" @tab-click="clickType">
                    <el-tab-pane label="推荐" :name="1" />
                    <el-tab-pane label="最新" :name="2" />
                    <el-tab-pane label="最热" :name="3" />
                </el-tabs>
            </div>
            <div class="blog-list">
                <div v-for="(item, index) in list" :key="index" class="blog-item" @click="clickItem(i)">
                    <el-image class="icon" :src="item.image" fit="fill" />
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
            for (var i = 0; i < 10; i += 1) {
                this.group.push({ id: i, label: '分类' + i, value: 'value' + i, time: 0, count: i + 3 });
            }
        },
        async loadList() {
            for (var i = 0; i < 10; i += 1) {
                this.list.push({ id: i, title: '标题' + i, summary: 'summary' + i, time: 1703145253000 });
            }
        },
        clickType() {
        },
        clickItem(item) {
            alert(item);
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