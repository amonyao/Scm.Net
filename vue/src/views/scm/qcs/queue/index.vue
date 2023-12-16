<template>
    <el-container>
        <scSearch>
            <template #search>
                <el-form ref="formRef" label-width="100px" :model="param" :inline="true">
                    <el-form-item label="排队方案" prop="detail_id">
                        <sc-select v-model="param.detail_id" :data="detail_list" placeholder="请选择">
                        </sc-select>
                    </el-form-item>
                    <el-form-item label="排队状态" prop="handle">
                        <sc-select v-model="param.handle" :data="handle_list" placeholder="请选择">
                        </sc-select>
                    </el-form-item>
                    <el-form-item label="数据状态" prop="row_status">
                        <sc-select v-model="param.row_status" :data="statusList" placeholder="请选择">
                        </sc-select>
                    </el-form-item>
                    <el-form-item label="创建时间" prop="create_time">
                        <el-date-picker v-model="param.create_time" type="datetimerange" range-separator="至"
                            start-placeholder="开始日期" end-placeholder="结束日期">
                        </el-date-picker>
                    </el-form-item>
                </el-form>
            </template>
        </scSearch>
        <el-header>
            <div class="left-panel">
                <el-button icon="el-icon-plus" type="primary" @click="open_dialog()" />
                <el-divider direction="vertical"></el-divider>
                <el-button-group>
                    <el-tooltip content="启用">
                        <el-button type="primary" icon="el-icon-circle-check" plain :disabled="selection.length == 0"
                            @click="status_list(1)"></el-button>
                    </el-tooltip>
                    <el-tooltip content="停用">
                        <el-button type="primary" icon="el-icon-circle-close" plain :disabled="selection.length == 0"
                            @click="status_list(2)"></el-button>
                    </el-tooltip>
                    <el-tooltip content="删除">
                        <el-button type="danger" icon="el-icon-delete" plain :disabled="selection.length == 0"
                            @click="delete_list"></el-button>
                    </el-tooltip>
                </el-button-group>
            </div>
            <div class="right-panel">
                <div class="right-panel-search">
                    <el-input v-model="param.key" clearable placeholder="关键字" />
                    <el-button icon="el-icon-search" type="primary" @click="search" />
                </div>
            </div>
        </el-header>
        <el-main class="nopadding">
            <scTable ref="table" :api-obj="apiObj" :column="column" row-key="id" @menu-handle="menuHandle"
                @selection-change="selectionChange">
                <el-table-column align="center" fixed type="selection" width="60" />
                <el-table-column label="#" type="index" width="50"></el-table-column>
                <el-table-column label="操作" align="center" fixed="right" width="140">
                    <template #default="scope">
                        <el-button text type="primary" size="small" @click="open_dialog(scope.row)">
                            编辑
                        </el-button>
                        <el-divider direction="vertical" />
                        <el-popconfirm title="确定删除吗？" @confirm="table_del(scope.row, scope.$index)">
                            <template #reference>
                                <el-button text type="primary" size="small">删除</el-button>
                            </template>
                        </el-popconfirm>
                    </template>
                </el-table-column>
                <template #row_status="scope">
                    <el-tooltip :content="scope.row.row_status ? '正常' : '停用'" placement="right">
                        <el-switch v-model="scope.row.row_status" :active-value="1" :inactive-value="2"
                            @change="status_item($event, scope.row)">
                        </el-switch>
                    </el-tooltip>
                </template>
            </scTable>
        </el-main>
        <edit ref="edit" @complete="complete" />
    </el-container>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        edit: defineAsyncComponent(() => import("./edit")),
    },
    data() {
        return {
            apiObj: this.$API.scmqcsqueue.page,
            list: [],
            param: {
                detail_id: '0',
                handle: 0,
                row_status: 1,
                create_time: '',
                key: ""
            },
            detail_list: [this.$SCM.OPTION_ALL],
            statusList: [this.$SCM.OPTION_ALL],
            handle_list: [this.$SCM.OPTION_ALL],
            selection: [],
            column: [
                { label: "id", prop: "id", hide: true },
                { label: "号码", prop: "codec", width: "120", align: 'left' },
                { label: "方案", prop: "detail_id", width: "120", align: 'left', formatter: this.getDetailNames },
                { label: "姓名", prop: "namec", width: "120", align: "left" },
                { label: "证件", prop: "label", minWidth: "120", align: "left" },
                { label: "电话", prop: "phone", width: "120", align: "left" },
                { label: "优先级", prop: "lv", width: "120", align: "left" },
                { label: "呼叫次数", prop: "qty", width: "80" },
                { label: "处理状态", prop: "handle", width: "120", formatter: this.getHandle },
                { label: "更新时间", prop: "update_time", width: "160", formatter: this.$TOOL.dateTimeFormat },
                { label: "创建时间", prop: "create_time", width: "160", formatter: this.$TOOL.dateTimeFormat },
            ],
        };
    },
    mounted() {
        this.$SCM.list_status(this.statusList, true);
        this.$SCM.list_dic(this.handle_list, 'qcs_handle', true);
        this.$SCM.list_option(this.detail_list, this.$API.scmqcsdetail.option, {}, true);
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
        getHandle(handle) {
            return this.$SCM.get_dic_names(this.handle_list, handle, '');
        },
        getDetailNames(detailId) {
            return this.$SCM.get_option_names(this.detail_list, detailId, '');
        }
    },
};
</script>
