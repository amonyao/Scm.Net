<template>
    <div class="doc-content-wrapper">
        <div class="doc-content-container" style="margin: 0 auto;">
            <div class="doc-content">
                <div>
                    <h1>ScFormTable用法</h1>
                    <p>用于显示一个列表数据。</p>
                    <sc-code :code="example1.code" title="基础用法" desc="用于显示一个列表数据。">
                        <sc-form-table v-model="example1.data.items" :addTemplate="example1.data.addTemplate">
                            <el-table-column prop="name" label="姓名">
                                <template #default="scope">
                                    <el-input v-model="scope.row.title" placeholder="请输入姓名" />
                                </template>
                            </el-table-column>
                            <el-table-column prop="sex" label="性别">
                                <template #default="scope">
                                    <sc-select v-model="scope.row.sex" :data="example1.data.sexs" />
                                </template>
                            </el-table-column>
                            <el-table-column prop="phone" label="电话">
                                <template #default="scope">
                                    <el-input v-model="scope.row.phone" placeholder="请输入电话" />
                                </template>
                            </el-table-column>
                        </sc-form-table>
                    </sc-code>
                    <h2>ScFormTable属性</h2>
                    <el-table :data="fieldTable" style="width: 100%">
                        <el-table-column prop="name" label="属性名" width="120" />
                        <el-table-column prop="desc" label="说明" />
                        <el-table-column prop="type" label="类型" width="80" />
                        <el-table-column prop="val" label="可选值" width="120" />
                        <el-table-column prop="def" label="默认值" width="120" />
                    </el-table>
                    <h2>ScList事件</h2>
                    <el-table :data="eventTable" style="width: 100%">
                        <el-table-column prop="name" label="事件名" width="120" />
                        <el-table-column prop="desc" label="说明" />
                        <el-table-column prop="args" label="回调参数" width="120" />
                    </el-table>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import "@/assets/code.scss";
import scCode from "@/components/scCode";

export default {
    components: {
        scCode: scCode
    },
    data() {
        return {
            example1: {
                code: `<sc-form-table v-model="example1.data.items" :addTemplate="example1.data.addTemplate">
	<el-table-column prop="name" label="姓名">
		<template #default="scope">
			<el-input v-model="scope.row.title" placeholder="请输入姓名" />
		</template>
	</el-table-column>
	<el-table-column prop="sex" label="性别">
		<template #default="scope">
			<sc-select v-model="scope.row.sex" :data="example1.data.sexs" />
		</template>
	</el-table-column>
	<el-table-column prop="phone" label="电话">
		<template #default="scope">
			<el-input v-model="scope.row.phone" placeholder="请输入电话" />
		</template>
	</el-table-column>
</sc-form-table>
export default{
    data(){
        return {
            sexs: [
                { id: 0, label: '请选择', value: 0 },
                { id: 1, label: '男', value: 1 },
                { id: 2, label: '女', value: 2 },
                { id: 3, label: '保密', value: 3 },
            ],
            items: [
                { id: '1', name: '张三', sex: 1, phone: '' },
                { id: '2', name: '李四', sex: 2, phone: '' },
                { id: '3', name: '王五', sex: 3, phone: '' }
            ],
            addTemplate: {
                id: '',
                name: '',
                sex: 0,
                phone: '',
            },
        }
    }
}
\x3c/script\x3e`,
                data: {
                    sexs: [
                        { id: 0, label: '请选择', value: 0 },
                        { id: 1, label: '男', value: 1 },
                        { id: 2, label: '女', value: 2 },
                        { id: 3, label: '保密', value: 3 },
                    ],
                    items: [
                        { id: '1', name: '张三', sex: 1, phone: '' },
                        { id: '2', name: '李四', sex: 2, phone: '' },
                        { id: '3', name: '王五', sex: 3, phone: '' }
                    ],
                    addTemplate: {
                        id: '',
                        name: '',
                        sex: 0,
                        phone: '',
                    },
                }
            },
            fieldTable: [
                { name: 'placeholder', type: 'String', desc: '数据为空时提示信息', val: '', def: '暂无数据' },
                { name: 'dragSort', type: 'Boolean', desc: '是否允许拖拽排序', val: '', def: 'false' },
                { name: 'isAdd', type: 'Boolean', desc: '是否允许新增数据', val: '', def: 'true' },
                { name: 'addTemplate', type: 'Object', desc: '新增行对象', val: '', def: '' }
            ],
            eventTable: [
                { name: 'change', desc: '选择项变化事件', args: 'item,index' },
                { name: 'editItem', desc: '列表项编辑事件', args: 'item,index' },
                { name: 'dropItem', desc: '列表项删除事件', args: 'item,index' },
            ]
        }
    },
    mounted() {
    },
    methods: {
    },
}
</script>