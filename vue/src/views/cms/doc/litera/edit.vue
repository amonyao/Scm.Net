<template>
    <sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
        <el-form ref="formRef" label-width="80px" :model="formData" :rules="rules">
            <el-row>
                <el-col :span="12">
                    <el-form-item label="分类" prop="cat_id">
                        <el-tree-select v-model="formData.cat_id" placeholder="请选择分类" :data="cat_list" collapse-tags
                            check-strictly :style="{ width: '100%' }" />
                    </el-form-item>
                    <el-form-item label="可见" prop="visible">
                        <sc-select v-model="formData.visible" :data="visible_list" placeholder="请输入可见"></sc-select>
                    </el-form-item>
                    <el-form-item label="批量添加" prop="batch">
                        <el-switch v-model="formData.batch" />
                    </el-form-item>
                    <el-form-item label="换行数量" prop="blank" v-if="this.formData.batch">
                        <el-input-number v-model="formData.blank"></el-input-number>
                    </el-form-item>
                </el-col>
                <el-col :span="12">
                    <el-form-item label="内容" prop="content">
                        <el-input v-model="formData.content" type="textarea" placeholder="请输入内容" :maxlength="256" :rows="16"
                            show-word-limit clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>

        <template #footer>
            <el-button @click="close">取 消</el-button>
            <el-button :loading="isSaveing" type="primary" @click="save">
                确 定
            </el-button>
        </template>
    </sc-dialog>
</template>
<script>
export default {
    data() {
        return {
            mode: "add",
            titleMap: { add: "新增网文", edit: "编辑网文" },
            visible: false,
            isSaveing: false,
            formData: this.def_data(),
            rules: {
                content: [
                    { required: true, trigger: "blur", message: "内容不能为空" },
                ],
            },
            cat_list: [this.$SCM.OPTION_ONE],
            visible_list: [this.$SCM.OPTION_ONE],
        };
    },
    mounted() {
        this.init();
        this.$SCM.list_dic(this.visible_list, 'article_visible', false);
    },
    methods: {
        def_data() {
            return {
                id: '0',
                types: '0',
                cat_id: '0',
                visible: '1',
                batch: false,
                blank: 1
            }
        },
        async init() {
            var res = await this.$API.cmsrescat.option.get();
            if (!res || res.code != 200) {
                return;
            }

            this.cat_list = this.$TOOL.changeTree(res.data);
        },
        async open(row) {
            if (!row || !row.id) {
                this.mode = "add";
            } else {
                this.mode = "edit";
                var res = await this.$API.cmsdoclitera.edit.get(row.id);
                this.formData = res.data;
            }
            this.visible = true;
        },
        save() {
            this.$refs.formRef.validate(async (valid) => {
                if (valid) {
                    this.isSaveing = true;
                    let res = null;
                    if (this.formData.id === '0') {
                        res = await this.$API.cmsdoclitera.add.post(this.formData);
                    } else {
                        res = await this.$API.cmsdoclitera.update.put(this.formData);
                    }
                    this.isSaveing = false;
                    if (res.code == 200) {
                        this.$emit("complete");
                        this.formData.content = '';
                        this.visible = this.formData.batch;
                        this.$message.success("保存成功");
                    } else {
                        this.$alert(res.message, "提示", { type: "error" });
                    }
                }
            });
        },
        close() {
            this.formData = this.def_data();
            this.$refs.formRef.resetFields();
            this.visible = false;
        },
    },
};
</script>

<style>
.el-select {
    width: 100%;
}
</style>