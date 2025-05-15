<template>
    <el-container>
        <el-main>
            123123
        </el-main>
        <el-aside width="280px">
            <el-container>
                <el-main class="nopadding">
                    <el-timeline>
                        <el-timeline-item placement="top" v-for="item in result_list" :key="item.id"
                            :timestamp="item.date">
                            <el-card shadow="never">
                                <h4>版本：{{ item.ver }}（BUILD:{{ item.build }}）</h4>
                                <p v-html="toHtml(item.remark)"></p>
                            </el-card>
                        </el-timeline-item>
                    </el-timeline>
                </el-main>
                <el-footer>
                    <el-button type="primary" @click="accept">通过</el-button>
                    <el-button @click="reject">拒绝</el-button>
                </el-footer>
            </el-container>
        </el-aside>
    </el-container>
</template>
<script>
export default {
    data() {
        return {
            title: '流程审批',
            visible: false,
            isSaveing: false,
            formData: {
                id: '0',
            },
            result_list: []
        };
    },
    mounted() {
    },
    methods: {
        async loadData(id) {
            if (!id) {
                return;
            }

            var res = await this.$API.samplesbook.view.get(id);
            this.formData = res.data || {};
            this.visible = true;
        },
        async loadResult() {

        },
        accept() {
            alert('accept');
        },
        reject() {
            alert('reject');
        }
    },
};
</script>