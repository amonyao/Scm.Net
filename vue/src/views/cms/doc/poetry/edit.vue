<template>
    <sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="780px" @close="close">
        <el-form ref="formRef" label-width="60px" :model="formData" :rules="rules">
            <el-row>
                <el-col :span="12">
                    <el-form-item label="标题" prop="title">
                        <el-input v-model="formData.title" placeholder="请输入标题" :maxlength="256" show-word-limit
                            clearable></el-input>
                    </el-form-item>
                    <el-form-item label="分类" prop="cat_id">
                        <el-tree-select v-model="formData.cat_id" placeholder="请选择分类" :data="cat_list" collapse-tags
                            check-strictly :style="{ width: '100%' }" />
                    </el-form-item>
                    <el-form-item label="国别" prop="nation_id">
                        <sc-select v-model="formData.nation_id" :data="nation_list" @change="getDynasty()"
                            placeholder="请输入国别"></sc-select>
                    </el-form-item>
                    <el-form-item label="朝代" prop="dynasty_id">
                        <sc-select v-model="formData.dynasty_id" :data="dynasty_list" @change="getAuthor()"
                            placeholder="请输入朝代"></sc-select>
                    </el-form-item>
                    <el-form-item label="作者" prop="author_id">
                        <sc-select v-model="formData.author_id" :data="author_list" @change="getOrigin()"
                            placeholder="请输入作者"></sc-select>
                    </el-form-item>
                    <el-form-item label="出处" prop="origin_id">
                        <sc-select v-model="formData.origin_id" :data="origin_list" placeholder="请输入出处"></sc-select>
                    </el-form-item>
                    <el-form-item label="可见" prop="visible">
                        <sc-select v-model="formData.visible" :data="visible_list" placeholder="请输入可见"></sc-select>
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
            titleMap: { add: "新增诗词", edit: "编辑诗词" },
            visible: false,
            isSaveing: false,
            formData: this.def_data(),
            rules: {
                title: [
                    { required: true, trigger: "blur", message: "标题不能为空" },
                ],
                content: [
                    { required: true, trigger: "blur", message: "内容不能为空" },
                ],
            },
            types_list: [this.$SCM.OPTION_ONE],
            cat_list: [this.$SCM.OPTION_ONE],
            nation_list: [this.$SCM.OPTION_ONE],
            dynasty_list: [this.$SCM.OPTION_ONE],
            author_list: [this.$SCM.OPTION_ONE],
            origin_list: [this.$SCM.OPTION_ONE],
            visible_list: [this.$SCM.OPTION_ONE],
        };
    },
    mounted() {
        this.init();
        this.getNation();
        this.$SCM.list_dic(this.visible_list, 'article_visible', false);
    },
    methods: {
        def_data() {
            return {
                id: '0',
                types: '0',
                cat_id: '0',
                title: '',
                nation_id: '0',
                dynasty_id: '0',
                author_id: '0',
                origin_id: '0',
                visible: '20',
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
                var res = await this.$API.cmsdocpoetry.edit.get(row.id);
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
                        res = await this.$API.cmsdocpoetry.add.post(this.formData);
                    } else {
                        res = await this.$API.cmsdocpoetry.update.put(this.formData);
                    }
                    this.isSaveing = false;
                    if (res.code == 200) {
                        this.$emit("complete");
                        this.visible = false;
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
        async getNation() {
            var res = await this.$API.cmsresnation.option.get();
            this.$SCM.prepare(this.nation_list, res, false);
        },
        async getDynasty() {
            if (!this.formData.nation_id) {
                return;
            }

            var res = await this.$API.cmsresdynasty.option.get({ 'nation_id': this.formData.nation_id });
            this.$SCM.prepare(this.dynasty_list, res, false);
        },
        async getAuthor() {
            if (!this.formData.dynasty_id) {
                return;
            }

            var res = await this.$API.cmsresauthor.option.get({ 'dynasty_id': this.formData.dynasty_id });
            this.$SCM.prepare(this.author_list, res, false);
        },
        async getOrigin() {
            if (!this.formData.author_id) {
                return;
            }

            var res = await this.$API.cmsresorigin.option.get({ 'author_id': this.formData.author_id });
            this.$SCM.prepare(this.origin_list, res, false);
        }
    },
};
</script>

<style>
.el-select {
    width: 100%;
}
</style>