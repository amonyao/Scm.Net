<template>
    <el-container>
        <el-main>
            <el-container style="background-color: #fff;">
                <el-header>
                    <el-row>
                        <el-col :span="8">
                            {{ header.codes }}
                        </el-col>
                        <el-col :span="8">
                            {{ flow_data.types }}/{{ flow_data.result }}
                        </el-col>
                    </el-row>
                </el-header>
                <el-main class="nopadding">
                    <scTable ref="table" :table-name="tableName" :data="detail_list" :column="column" row-key="id"
                        :hidePagination="true" :hideDo="true">
                        <el-table-column label="#" type="index" width="50"></el-table-column>
                    </scTable>
                </el-main>
            </el-container>
        </el-main>
        <el-aside width="280px">
            <el-container>
                <el-main class="nopadding">
                    <el-timeline>
                        <el-timeline-item placement="top" v-for="item in result_list" :key="item.id"
                            :timestamp="item.date">
                            <el-card shadow="never">
                                <h4>{{ this.$TOOL.dateTimeFormat(item.create_time) }}</h4>
                                <p v-html="item.comment"></p>
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
import { useRoute } from "vue-router";

export default {
    data() {
        return {
            title: '流程审批',
            tableName: 'samples_po_flow',
            apiObj: this.$API.samplespodetail.page,
            param: {
                id: '0',
                row_status: 1,
                create_time: '',
                key: ''
            },
            selection: [],
            column: [
                { label: "id", prop: "id", hide: true },
                { prop: 'book_codes', label: '书籍编码', width: 100 },
                { prop: 'book_names', label: '书籍名称', width: 140, align: 'left' },
                { prop: 'need_qty', label: '需求数量', width: 120, align: 'right' },
                { prop: 'real_qty', label: '实际数量', width: 100, align: 'right' },
                { prop: 'remark', label: '备注', minWidth: 140, align: 'left' },
                { prop: "update_names", label: "更新人员", width: 100, },
                { prop: "update_time", label: "更新时间", width: 160, formatter: this.$TOOL.dateTimeFormat },
                { prop: "create_names", label: "创建人员", width: 100, },
                { prop: "create_time", label: "创建时间", width: 160, formatter: this.$TOOL.dateTimeFormat },
            ],
            id: '0',
            header: {},
            detail_list: [],
            flow_data: {},
            result_list: []
        };
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            var route = useRoute();
            var id = route.query.id;
            console.log(id);
            if (!this.$SCM.is_valid_id(id)) {
                console.log('无效的id');
                return;
            }

            this.id = id;

            var res = await this.$API.sysflowdata.view.get(id);
            if (!res || res.code != 200) {
                return;
            }
            this.flow_data = res.data;

            res = await this.$API.samplespoheader.view.get(res.data.order_id);
            if (!res || res.code != 200) {
                return;
            }

            this.header = res.data;
            this.param.id = this.header.id;
            this.listDetail();
            this.listResult();
        },
        async listDetail() {
            var res = await this.$API.samplespodetail.list.get(this.param);
            if (!res || res.code != 200) {
                return;
            }
            this.detail_list = res.data;
        },
        async listResult() {
            var res = await this.$API.sysflowdata.result.get(this.id);
            if (!res || res.code != 200) {
                return;
            }
            this.result_list = res.data;
        },
        complete() {
            this.listDetail();
        },
        search() {
            this.listDetail();
        },
        async accept() {
            var data = { 'id': this.id, 'comment': '' };
            var res = await this.$API.samplespoheader.accept.post(data);
            if (!res || res.code != 200) {
                return;
            }

            this.$message.success("审批通过");
        },
        async reject() {
            var data = { 'id': this.id, 'comment': '' };
            var res = await this.$API.samplespoheader.reject.post(data);
            if (!res || res.code != 200) {
                return;
            }

            this.$message.success("审批拒绝");
        }
    },
};
</script>