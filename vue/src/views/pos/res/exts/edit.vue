<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="商品ID" prop="spu_id"> 
	<el-input 
		v-model="formData.spu_id" 
		placeholder="请输入商品ID" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="显示排序" prop="od"> 
	<el-input 
		v-model="formData.od" 
		placeholder="请输入显示排序" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="属性名称" prop="namec"> 
	<el-input 
		v-model="formData.namec" 
		placeholder="请输入属性名称" 
		:maxlength="32" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="属性内容" prop="json"> 
	<el-input 
		v-model="formData.json" 
		placeholder="请输入属性内容" 
		:maxlength="512" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="单价" prop="price"> 
	<el-input 
		v-model="formData.price" 
		placeholder="请输入单价" 
		:maxlength="11" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 
<el-form-item label="创建时间" prop="creaet_time"> 
	<el-input 
		v-model="formData.creaet_time" 
		placeholder="请输入创建时间" 
		:maxlength="20" 
		show-word-limit 
		clearable 
	></el-input> 
</el-form-item> 

		</el-form>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</sc-dialog>
</template>
<script>
export default {
	data() {
		return {
			mode: "add",
			titleMap: {add: "新增",edit: "编辑"},
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {},
		};
	},
	mounted() {
	},
	methods: {
	    def_data(){
		    return {
				id: '0',
				spu_id:'', 
od:'', 
namec:'', 
json:'', 
price:'', 
creaet_time:'', 

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.posresspuexts.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.posresspuexts.add.post(this.formData);
					} else {
						res = await this.$API.posresspuexts.update.put(this.formData);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("保存成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = this.def_data();
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>

<style>
.el-select {
	width: 100%;
}
</style>