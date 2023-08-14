<template>
    <sc-waterfall></sc-waterfall>
</template>
<script>
import scWaterfall from '@/components/scWaterfall'

export default {
    components: {
        scWaterfall,
    },
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
