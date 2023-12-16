<template>
    <el-container>
        <el-main>
            <el-card style="max-width: 960px; margin: 0 auto;">
                <el-timeline>
                    <el-timeline-item placement="top" v-for="item in ver_list" :key="item.id" :timestamp="item.date">
                        <el-card shadow="never">
                            <h4>版本：{{ item.ver }}（BUILD:{{ item.build }}）</h4>
                            <p v-html="toHtml(item.remark)"></p>
                        </el-card>
                    </el-timeline-item>
                </el-timeline>
            </el-card>
        </el-main>
    </el-container>
</template>

<script>
export default {
    data() {
        return {
            ver_list: []
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            var res = await this.$API.devversion.list.get({ types: 10 });
            if (res == null || res.code != 200) {
                return;
            }

            this.ver_list = res.data;
        },
        toHtml(html) {
            if (!html) {
                return '';
            }
            return html.replace(/\n/g, '<br/>');
        }
    }
}
</script>
