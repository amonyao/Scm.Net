<template>
    <el-container>
        <el-aside width="260px">
            <el-container>
                <el-header>
                    <el-tree-select placeholder="请选择上级方案" clearable filterable default-expand-all :check-strictly="true"
                        highlight-current :indent="24" v-model="param.header_id" :data="tree" @change="changeHeader" />
                </el-header>
                <el-main class="nopadding">
                    <sc-list :data="list" @change="changeDetail">
                        <template #item="{ item }">
                            <div>{{ item.namec }}</div>
                        </template>
                    </sc-list>
                </el-main>
            </el-container>
        </el-aside>
        <el-main class="nopadding">
            <el-container>
                <el-header>
                    <div class="left-panel">
                        <el-button type="primary" plain :disabled="param.detail_id == '0'" @click="next()">下一位</el-button>
                        <el-button type="danger" plain :disabled="param.detail_id == '0'" @click="skip()">跳过</el-button>
                        <el-button type="danger" plain @click="exportAll()">导出</el-button>
                    </div>
                    <div class="right-panel">
                        <div class="right-panel-search">
                            <el-input v-model="param.key" clearable placeholder="登录账号" />
                            <el-button icon="el-icon-search" type="primary" @click="search" />
                        </div>
                    </div>
                </el-header>
                <el-main class="nopadding">
                    <scTable ref="table" :api-obj="apiObj" :column="column" row-key="id" @menu-handle="menuHandle"
                        @selection-change="selectionChange">
                        <!-- 固定列-选择列 -->
                        <el-table-column fixed type="selection" width="60" />
                        <el-table-column label="#" type="index" width="50"></el-table-column>
                        <el-table-column align="center" fixed="right" label="操作" width="80">
                            <template #default="scope">
                                <el-button size="small" text type="primary" @click="open_dialog(scope.row)">
                                    呼叫
                                </el-button>
                            </template>
                        </el-table-column>
                        <template #row_status="scope">
                            <el-tooltip :content="scope.row.row_status ? '正常' : '停用'" placement="right">
                                <el-switch v-model="scope.row.row_status" :active-value="1" :inactive-value="2"
                                    @change="status_item($event, scope.row)">
                                </el-switch>
                            </el-tooltip>
                        </template>
                        <template #avatar="{ data }">
                            <el-avatar :src="$CONFIG.SERVER_URL + data.avatar" size="small"></el-avatar>
                        </template>
                        <template #OrganizeObj="{ data }">
                            {{ data.organizeObj?.name }}
                        </template>
                    </scTable>
                </el-main>
            </el-container>
        </el-main>
    </el-container>
</template>
<script>
const signalR = require("@microsoft/signalr");
export default {
    data() {
        return {
            apiObj: this.$API.scmqcsqueue.page,
            tree: [],
            list: [],
            param: {
                header_id: '0',
                detail_id: '0',
                key: "",
            },
            selection: [],
            column: [
                { label: "id", prop: "id", hide: true },
                { label: "号码", prop: "codec", width: "120", align: 'left' },
                { label: "姓名", prop: "contact", width: "120", align: "left" },
                { label: "证件", prop: "label", width: "120", align: "left" },
                { label: "电话", prop: "phone", width: "120", align: "left" },
                { label: "优先级", prop: "level", width: "120", align: "left" },
                { label: "所属部门", prop: "OrganizeObj", align: "left", width: "200" },
                { label: "呼叫次数", prop: "calling", width: "80" },
                { label: "处理状态", prop: "handle", width: "120" },
                { label: "更新时间", prop: "update_time", width: "160", formatter: this.$TOOL.dateTimeFormat },
                { label: "创建时间", prop: "create_time", width: "160", formatter: this.$TOOL.dateTimeFormat },
            ],
        };
    },
    mounted() {
        this.initTree({});
        this.init();
    },
    methods: {
        complete() {
            this.$refs.table.refresh();
        },
        search() {
            this.$refs.table.upData(this.param);
        },
        async status_item(e, row) {
            this.$SCM.status_item(this, this.$API.scmqcsqueue.status, row, row.row_status);
        },
        status_list(status) {
            this.$SCM.status_list(this, this.$API.scmqcsqueue.status, this.selection, status);
        },
        async delete_item(row) {
            this.$SCM.delete_item(this, this.$API.scmqcsqueue.delete, row);
        },
        delete_list() {
            this.$SCM.delete_list(this, this.$API.scmqcsqueue.delete, this.selection);
        },
        open_dialog(row) {
            this.$refs.edit.open(row);
        },
        selectionChange(selection) {
            this.selection = selection;
        },
        menuHandle(obj) {
            if (obj.command == "add") {
                this.open_dialog();
                return;
            }
            if (obj.command == "edit") {
                this.open_dialog(obj.row);
                return;
            }
            if (obj.command == "delete") {
                this.delete_item(obj.row);
                return;
            }
        },
        async initTree(param) {
            const t = await this.$API.scmqcsheader.list.get(param);
            let _tree = [{ 'id': '1', value: '0', label: '请选择' }];
            t.data.some((m) => {
                _tree.push({
                    id: m.id,
                    value: m.id,
                    label: m.namec,
                    parentId: m.pid,
                });
            });
            this.tree = this.$TOOL.changeTree(_tree);
        },
        //树点击事件
        changeHeader() {
            this.listDetail({ id: this.param.header_id });
        },
        // 获取队列
        async listDetail(param) {
            var res = await this.$API.scmqcsdetail.list.get(param);
            if (!res || res.code != 200) {
                return;
            }
            this.list = res.data;
        },
        changeDetail(row) {
            if (!row) {
                return;
            }
            this.param.detail_id = row.id;
            this.search();
        },
        init() {
            var url = this.$CONFIG.SERVER_URL + '/QcsHub';
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(url)
                .build();

            this.connection.start().then(() => {
                this.connection.on("ReceiveMessage", (id, qty, idx) => {
                    this.messages.push(`${id}: ${qty}: ${idx}`);
                });
            });
        },
        sendMessage(dvo) {
            this.$message.success("取号成功：" + dvo.codec);
            if (dvo) {
                this.connection.invoke("SendMessage", dvo.detail_id);
            }
        },
        async next() {
            if (this.param.detail_id === '0') {
                return;
            }

            var res = await this.$API.scmqcsqueue.calling.get({ id: this.param.detail_id, dir: 3 });
            if (!res || res.code != 200) {
                return;
            }
            this.sendMessage(res.data);
        },
        skip() {

        }
    },
};
</script>
