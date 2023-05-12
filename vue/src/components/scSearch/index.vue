<template>
    <div class="sc_search_card" v-if="visible">
        <div class="sc_search" v-if="visible && searchVisible">
            <slot name="search"></slot>
        </div>
        <div class="sc_filter" v-if="visible && filterVisible">
            <div class="left-panel">
                <slot name="filter_left"></slot>
            </div>
            <div class="right-panel">
                <slot name="filter_right"></slot>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        visible: { type: Boolean, default: true },
        searchVisible: { type: Boolean, default: true },
        filterVisible: { type: Boolean, default: true },
    },
    data() {
        return {

        }
    }
}
</script>

<style scoped>
.sc_search_card {
    height: auto;
    display: inline;
    padding-top: 18px;
    background-color: white;
}

.sc_search {}

.sc_filter {
    display: flex;
    justify-content: space-between;
}
</style>
  