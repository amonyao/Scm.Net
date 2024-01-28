<template>
    <scWoodenfish />
</template>
<script>
import { defineAsyncComponent } from "vue";
export default {
    components: {
        scWoodenfish: defineAsyncComponent(() => import("@/components/scWoodenfish")),
    },
    data() {
        return {
        }
    },
}
</script>
