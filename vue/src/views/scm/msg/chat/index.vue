<template>
    <el-container>
        <el-aside>
            <div class="sidebar-wrap">
                <bar-tab class="width-80" />
                <bar-page />
            </div>
        </el-aside>
        <el-main>

        </el-main>
    </el-container>
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        barTab: defineAsyncComponent(() => import("./components/barTab")),
        barPage: defineAsyncComponent(() => import("./components/barPage")),
    },
    data() {
        return {
            view: null,
        }
    },
    methods: {

    }
}
</script>
<style lang="scss" scoped>
.sidebar-wrap {
    background-color: #363e47;
    display: flex;
    width: 100%;
}

.width-80 {
    width: 80px;
}
</style>