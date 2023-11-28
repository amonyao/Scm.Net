<template>
	<el-container>
		<el-main>
			<el-card>
				<sc-list :data="list" :hideDo="true">
					<template #item="{ item }">
						<sc-summary :title="item.title" :summary="item.summary" @click="openItem(item)"></sc-summary>
					</template>
				</sc-list>
			</el-card>
		</el-main>
	</el-container>
</template>
<script>
export default {
	data() {
		return {
			list: [],
			param: {
				key: ''
			},
		};
	},
	mounted() {
		this.search();
	},
	methods: {
		async search() {
			var res = await this.$API.cmsdocarticleshare.page.get(this.param);
			if (!res || res.code != 200) {
				return;
			}
			this.list = res.data.items;
		},
		openItem(item) {
			this.$router.push({ path: '/cms/doc/items', query: { 'id': item.ref_id } });
		},
	},
};
</script>
