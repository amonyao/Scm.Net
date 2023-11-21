<template>
    <div ref="fullScreen" class="dd">
        <div class="sc-panel panel-default">
            <div class="sc-head">
                <el-row>
                    <el-col :span="16">
                        <div class="title">{{ title }}</div>
                    </el-col>
                    <el-col :span="8" style="text-align: right;">
                        <el-button type="default" icon="el-icon-full-screen" circle @click="showFullPage()"
                            v-show="!fullScreen && !fullPage" title="全窗口显示"></el-button>
                        <el-button type="default" icon="el-icon-full-screen" circle @click="exitFullPage()"
                            v-show="!fullScreen && fullPage" title="退出全窗口"></el-button>
                        <el-button type="default" icon="el-icon-full-screen" circle @click="showFullScreen()"
                            v-show="!fullScreen" title="全屏幕显示"></el-button>
                        <el-button type="default" icon="el-icon-full-screen" circle @click="exitFullScreen()"
                            v-show="fullScreen" title="退出全屏幕"></el-button>
                        <el-button type="default" icon="el-icon-info-filled" circle @click="showDrawer()" title="帮助"></el-button>
                    </el-col>
                </el-row>
                <slot name="head"></slot>
            </div>
            <div class="sc-body">
                <slot></slot>
            </div>
        </div>
    </div>
    <el-drawer v-model="drawer" title="提示" direction="btt">
        <div class="panel-drawer">
            <span>{{ remark }}</span>
        </div>
    </el-drawer>
</template>
<script>
export default {
    name: "ScPanel",
    data() {
        return {
            fullPage: false,
            fullScreen: false,
            drawer: false,
        }
    },
    props: {
        title: { type: String, default: '卡片标题' },
        remark: { type: String, default: '这是工具说明！' },
    },
    methods: {
        showDrawer() {
            this.drawer = true;
        },
        showFullPage() {
            this.$TOOL.showFullPage(this.$refs.fullScreen);
            this.fullPage = true;
        },
        exitFullPage() {
            this.$TOOL.exitFullPage(this.$refs.fullScreen);
            this.fullPage = false;
        },
        showFullScreen() {
            this.$TOOL.screen(this.$refs.fullScreen);
            this.fullScreen = true;
        },
        exitFullScreen() {
            this.$TOOL.screen(this.$refs.fullScreen);
            this.fullScreen = false;
        }
    }
}
</script>
  
<style scoped>
.dd {
    height: 100%;
    padding: 15px;
    background-color: white;
}

.sc-panel {
    height: 100%;
    margin: 0px;
    background-color: #fff;
    border: 1px solid transparent;
    border-radius: 4px;
    -webkit-box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
    box-shadow: 0 1px 1px rgba(0, 0, 0, .05);
}

.sc-head {
    margin: 0px;
    padding: 10px 15px;
    border: 0px;
    border-bottom: 1px solid transparent;
    border-radius: 0px;
    border-top-left-radius: 3px;
    border-top-right-radius: 3px;
}

.title {
    margin: 3px 0px;
    padding: 0px;
    display: inline-block;
    font-size: 18px;
}

.sc-panel .sc-body {
    padding: 15px;
}

.panel-default {
    border-color: #ddd;
}

.panel-default>.sc-head {
    color: #333;
    background-color: #f5f5f5;
    border-color: #ddd;
}

.panel-drawer {
    overflow: scroll-y;
    min-height: 60px;
    margin: 0px 10px;
}
</style>