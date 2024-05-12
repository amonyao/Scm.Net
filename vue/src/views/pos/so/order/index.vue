<template>
    <el-container>
        <el-aside>
            <sc-list></sc-list>
        </el-aside>
        <el-main>
            <div class="grid">
                <el-card>
                    123123
                </el-card>
                <el-card>
                    123123
                </el-card>
                <el-card>
                    123123
                </el-card>
                <el-card>
                    123123
                </el-card>
            </div>
        </el-main>
        <el-aside>
            <sc-list></sc-list>
        </el-aside>
    </el-container>
</template>
<style scoped>
.grid {
    display: grid;
    grid-template-columns: repeat(5, 16%);
    grid-row-gap: 5%;
    grid-column-gap: 5%;
}
</style>