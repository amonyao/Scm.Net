<template>
    <el-button v-bind="$attrs"></el-button>
</template>

<script>
export default {
    props: {
        icon: { type: String, default: null }
    },
    data() {
        return {
        }
    }
}
</script>
