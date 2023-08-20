<template>
    <sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
        <el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
            <el-form-item label="标题" prop="title">
                <el-input v-model="formData.title" placeholder="请输入标题" :maxlength="256" show-word-limit
                    clearable></el-input>
            </el-form-item>
            <el-form-item label="分类" prop="cat_id">
                <el-input v-model="formData.cat_id" placeholder="请输入分类" :maxlength="20" show-word-limit
                    clearable></el-input>
            </el-form-item>
            <el-form-item label="国别" prop="nation_id">
                <sc-select v-model="formData.nation_id" placeholder="请输入国别"></sc-select>
            </el-form-item>
            <el-form-item label="朝代" prop="dynasty_id">
                <sc-select v-model="formData.dynasty_id" placeholder="请输入朝代"></sc-select>
            </el-form-item>
            <el-form-item label="作者" prop="author_id">
                <sc-select v-model="formData.author_id" placeholder="请输入作者"></sc-select>
            </el-form-item>
            <el-form-item label="出处" prop="origin_id">
                <sc-select v-model="formData.origin_id" placeholder="请输入出处"></sc-select>
            </el-form-item>
            <el-form-item label="可见" prop="public_types">
                <sc-select v-model="formData.public_types" placeholder="请输入可见"></sc-select>
            </el-form-item>
            <el-form-item label="内容" prop="content">
                <el-input v-model="formData.content" placeholder="请输入内容" :maxlength="256" show-word-limit
                    clearable></el-input>
            </el-form-item>
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
                codec: [
                    { required: true, trigger: "blur", message: "编码不能为空" },
                ],
            },
        };
    },
    mounted() {
    },
    methods: {
        def_data() {
            return {
                id: 0,
                key: '',
                salt: '',
                types: '',
                title: '',
                sub_title: '',
                qty: '',
                qty0: '',
                fav_qty: '',
                msg_qty: '',
                cat_id: '',
                nation_id: '',
                dynasty_id: '',
                author_id: '',
                origin_id: '',
                style_id: '',
                image: '',
                files: '',
                back_color: '',
                back_image: '',
                text_color: '',
                font_name: '',
                font_size: '',
                layout: '',
                origin_types: '',
                public_types: '',
            }
        },
        async open(row) {
            if (!row || !row.id) {
                this.mode = "add";
            } else {
                this.mode = "edit";
                var res = await this.$API.cmsdocarticle.edit.get(row.id);
                this.formData = res.data;
            }
            this.visible = true;
        },
        save() {
            this.$refs.formRef.validate(async (valid) => {
                if (valid) {
                    this.isSaveing = true;
                    let res = null;
                    if (this.formData.id === 0) {
                        res = await this.$API.cmsdocarticle.add.post(this.formData);
                    } else {
                        res = await this.$API.cmsdocarticle.update.put(this.formData);
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
    },
};
</script>

<style>
.el-select {
    width: 100%;
}
</style>