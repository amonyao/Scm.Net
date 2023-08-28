<template>
    <el-container>
        <el-main style="background-color: rebeccapurple;">
            <sc-waterfall style="background-color: antiquewhite;" @showItem="showItem" v-slot="{ data }">
                <img :data-src="data.src" :alt="data.names" :style="{ 'height': data._height + 'px' }">
            </sc-waterfall>
            <waterfall :line-gap="200" :watch="items">
                <!-- each component is wrapped by a waterfall slot -->
                <waterfall-slot v-for="(item, index) in items" :width="item.width" :height="item.height" :order="index"
                    :key="item.id">
                    <!--
                        your component
                    -->
                </waterfall-slot>
            </waterfall>
        </el-main>
    </el-container>
</template>
<script>
import scWaterfall from '@/components/scWaterfall'
import Waterfall from '@/components/scWaterfall/waterfall.vue'
import WaterfallSlot from '@/components/scWaterfall/waterfall-slot.vue'

export default {
    components: {
        scWaterfall,
        Waterfall,
        WaterfallSlot
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

        },
        showItem(dom, data) {
            var img = dom.children[0];
            if (!img) {
                return;
            }
            img.src = img.getAttribute('data-src');
            console.log('data:' + data);
        }
    },
};
</script>
