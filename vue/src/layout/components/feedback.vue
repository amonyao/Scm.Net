<template>
    <el-form ref="form" label-width="80px" label-position="left" style="padding:0 20px;">
        <el-form-item label="网页地址" prop="url">
            <el-input v-model="formData.url" show-word-limit clearable disabled></el-input>
        </el-form-item>
        <el-form-item label="问题类型" prop="types">
            <sc-select v-model="formData.types" :data="types_list" placeholder="请选择问题类型" collapse-tags></sc-select>
        </el-form-item>
        <el-form-item label="回馈事项" prop="title">
            <el-input v-model="formData.title" placeholder="请输入回馈事项" :maxlength="32" show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item label="详细说明" prop="remark">
            <el-input v-model="formData.remark" type="textarea" placeholder="请输入详细说明" :maxlength="1024" rows="5"
                show-word-limit clearable></el-input>
        </el-form-item>
        <el-form-item>
            <el-button @click="close">取 消</el-button>
            <el-button :loading="isSaveing" type="primary" @click="save">
                确 定
            </el-button>
        </el-form-item>
    </el-form>
</template>

<script>
export default {
    data() {
        return {
            types_list: [],
            isSaveing: false,
            formData: {
                url: this.$route.path,
                title: "",
                types: '0',
                remark: ""
            }
        }
    },
    mounted() {
        this.init();
    },
    methods: {
        async init() {
            this.$SCM.list_dic(this.types_list, 'feedback-type', false);
        },
        close() {
        },
        save() {

        }
    }
}
</script>
<style>
.el-select {
    width: 100%;
}
</style>