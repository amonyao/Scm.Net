<template>
    <el-upload action="http://localhost:5000/api/SamplesDemo/Upload" :show-file-list="false" :on-success="handleSuccess">
        <img v-if="imageUrl" :src="imageUrl" class="avatar" />
        <div v-else>
            <i class="el-icon-plus avatar-uploader-icon"></i>
            <div class="el-upload__text">点击上传封面</div>
        </div>
        <template #tip>
            <div v-if="!imageUrl" class="el-upload__tip">只能上传 jpg/png 文件，且不超过 500kb</div>
        </template>
    </el-upload>
</template>
<script>
export default {
    data() {
        return {
            mode: "add",
            titleMap: { add: "上传文件", },
            isSaveing: false,
            visible: false,
            path: undefined,
            rules: {},
            form: {
                file: "",
            },
            newDic: "",
            imageUrl: '',
        };
    },
    mounted() { },
    methods: {
        async open(row) {
            this.visible = true;
            this.newDic = row;
        },
        uploadSuccess() {
            this.$emit("complete");
            this.visible = false;
        },
        close() {
            this.visible = false;
        },
        handleSuccess() {

        }
    },
};
</script>
