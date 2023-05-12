<template>
    <el-form ref="form" label-width="120px" label-position="left" style="padding:0 20px;">
        <el-form-item label="回馈事项" prop="title">
            <el-input v-model="formData.title" placeholder="请输入展示姓名" :maxlength="32" show-word-limit clearable
                :style="{ width: '100%' }"></el-input>
        </el-form-item>
        <el-form-item label="问题类型" prop="types">
            <sc-select v-model="formData.types" :apiObj="$API.ur_position.list" placeholder="请选择问题类型" multiple collapse-tags
                style="width: 100%"></sc-select>
        </el-form-item>
        <el-form-item label="详细说明" prop="remark">
            <el-input v-model="formData.remark" type="textarea" placeholder="请输入展示姓名" :maxlength="32" show-word-limit
                clearable :style="{ width: '100%' }"></el-input>
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
            typeList: [],
            formData: {
                title: "",
                types: 0,
                remark: ""
            }
        }
    }
}
</script>