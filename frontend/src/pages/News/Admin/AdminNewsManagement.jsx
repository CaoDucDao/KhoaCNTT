import React, { useState, useEffect } from 'react'
import { useNavigate, Link } from 'react-router-dom'
import newsApi from '../../../api/newsApi'
import { formatDate } from '../../../helpers/newsHelpers'

const HomePage = () => {
	const navigate = useNavigate()
	const [news, setNews] = useState([])
	const [searchKeyword, setSearchKeyword] = useState('')

	useEffect(() => {
		newsApi
			.search({ pageSize: 5 })
			.then((res) => {
				const data = res.items ?? res ?? []
				setNews(data)
			})
			.catch(() => setNews([]))
	}, [])

	const fallbackNews = [
		{
			newsID: 1,
			title: 'Hội thảo Khoa học Công nghệ Sinh viên lần thứ XV - Năm 2026',
			createdAt: '2026-03-15'
		},
		{
			newsID: 2,
			title: 'Lịch bảo vệ Đồ án tốt nghiệp đợt 1 năm 2026 dành cho K62',
			createdAt: '2026-03-12'
		},
		{
			newsID: 3,
			title: 'Thông báo tuyển dụng thực tập sinh FPT Software 2026',
			createdAt: '2026-03-10'
		},
		{
			newsID: 4,
			title: 'Kết quả kỳ thi Olympic Tin học cấp trường năm học 2025-2026',
			createdAt: '2026-03-05'
		},
		{
			newsID: 5,
			title: 'Thông báo tuyển sinh Đợt 1 năm 2026 dành cho K62',
			createdAt: '2026-03-15'
		}
	]

	const displayNews = news.length > 0 ? news : fallbackNews

	const featured = displayNews[0]
	const largeNews = displayNews[1] || displayNews[0]
	const smallNews = displayNews.slice(2, 5)

	const handleSearch = (e) => {
		e.preventDefault()
		if (searchKeyword.trim()) {
			navigate(`/news?keyword=${encodeURIComponent(searchKeyword)}`)
		}
	}

	return (
		<div>
			{/* HERO BANNER */}
			<div className='bg-gradient-to-br from-[#1f4c7a] to-[#0f172a] text-white py-20 px-4'>
				<div className='max-w-7xl mx-auto'>
					<span className='inline-flex items-center gap-1.5 bg-red-500 text-white text-xs font-bold px-3 py-1 rounded-full mb-4'>
						📢 TIN NỔI BẬT
					</span>
					<h1 className='text-4xl font-bold leading-tight max-w-2xl mb-4'>
						{featured?.title ||
							'Chào mừng Tân sinh viên K66 Nhập học năm 2026'}
					</h1>
					<p className='text-gray-300 max-w-xl mb-8 line-clamp-3'>
						Khoa Công nghệ thông tin - Đại học Thủy Lợi tự hào là
						cái nôi đào tạo nguồn nhân lực chất lượng cao, đáp ứng
						nhu cầu khắt khe của kỷ nguyên số.
					</p>
					<button
						onClick={() =>
							featured &&
							navigate(`/news/${featured.newsID}`, {
								state: featured
							})
						}
						className='bg-red-500 hover:bg-red-600 text-white px-6 py-3 rounded-lg font-medium transition flex items-center gap-2'>
						Xem chi tiết →
					</button>
				</div>
			</div>

			{/* PHẦN TIN TỨC & SỰ KIỆN - NỀN ĐEN */}
			<div className='bg-white py-16'>
				<div className='max-w-7xl mx-auto px-4'>
					{/* Header row - mirrors the 3-column layout below */}
					<div className='flex gap-8 mb-8'>
						{/* Tiêu đề căn theo card lớn */}
						<div className='flex-1'>
							<h2 className='text-3xl font-bold text-white border-l-4 border-red-500 pl-4'>
								Tin tức & Sự kiện
							</h2>
						</div>
						{/* "Xem tất cả" căn theo cột 3 card nhỏ */}
						<div className='w-[340px] flex-shrink-0 flex justify-end items-center'>
							<Link
								to='/news'
								className='text-blue-400 hover:text-white transition text-sm font-medium'>
								Xem tất cả →
							</Link>
						</div>
						{/* Spacer căn theo widget bên phải */}
						<div className='w-52 flex-shrink-0' />
					</div>

					<div className='flex gap-8'>
						{/* CARD LỚN */}
						{largeNews && (
							<div
								onClick={() =>
									navigate(`/news/${largeNews.newsID}`, {
										state: largeNews
									})
								}
								className='flex-1 bg-white rounded-3xl overflow-hidden shadow-2xl relative h-[380px] cursor-pointer group'>
								<div className='h-[55%] bg-gradient-to-br from-slate-100 to-white flex items-center justify-center'>
									<span className='text-7xl text-gray-300'>
										📰
									</span>
								</div>
								<div className='absolute bottom-0 left-0 right-0 h-1/2 bg-gradient-to-t from-black/95 via-black/80 to-transparent' />
								<div className='absolute bottom-10 left-8 right-8 text-white z-10'>
									<div className='flex items-center gap-2 text-yellow-300 text-sm font-medium mb-3'>
										{formatDate(largeNews.createdAt)} • SỰ
										KIỆN
									</div>
									<h3 className='text-3xl font-bold leading-tight group-hover:text-yellow-300 transition'>
										{largeNews.title}
									</h3>
								</div>
							</div>
						)}

						{/* 3 CARD NHỎ */}
						<div className='w-[340px] flex-shrink-0 space-y-4'>
							{smallNews.map((item) => (
								<div
									key={item.newsID}
									onClick={() =>
										navigate(`/news/${item.newsID}`, {
											state: item
										})
									}
									className='bg-white rounded-3xl overflow-hidden shadow-xl border border-gray-100 cursor-pointer group'>
									<div className='h-28 bg-gray-200 flex items-center justify-center'>
										<span className='text-5xl text-gray-400'>
											📅
										</span>
									</div>
									<div className='p-5'>
										<div className='text-xs text-red-600 font-semibold mb-2'>
											{formatDate(item.createdAt)}
										</div>
										<h4 className='font-semibold text-gray-900 group-hover:text-[#1f4c7a] transition line-clamp-3'>
											{item.title}
										</h4>
									</div>
								</div>
							))}
						</div>

						{/* WIDGET BÊN PHẢI */}
						<div className='w-52 flex-shrink-0 space-y-3'>
							<form
								onSubmit={handleSearch}
								className='flex items-center border border-gray-200 rounded-xl overflow-hidden bg-white shadow-sm'>
								<input
									type='text'
									value={searchKeyword}
									onChange={(e) =>
										setSearchKeyword(e.target.value)
									}
									placeholder='Tìm kiếm...'
									className='px-3 py-2 text-xs text-gray-600 outline-none w-full'
								/>
								<button
									type='submit'
									className='bg-[#1f4c7a] px-3 py-2 text-white text-xs'>
									🔍
								</button>
							</form>

							<div className='bg-blue-50 rounded-2xl shadow-sm border border-blue-200 p-4'>
								<h3 className='font-bold text-blue-800 mb-1.5 flex items-center gap-1.5 text-sm'>
									🎓 Tra cứu học tập
								</h3>
								<p className='text-xs text-blue-600 mb-3'>
									Đăng nhập để xem điểm, thời khóa biểu và tài
									liệu nội bộ.
								</p>
								<Link
									to='/student-portal'
									className='block w-full bg-[#1f4c7a] text-white text-xs font-medium text-center py-2 rounded-xl hover:bg-[#163a5d] transition'>
									Truy cập Cổng Sinh Viên
								</Link>
							</div>

							<div className='bg-white rounded-2xl shadow-sm border border-gray-100 p-5'>
								<h3 className='font-bold text-gray-800 mb-4 flex items-center gap-1.5 text-sm'>
									🔗 Liên kết nhanh
								</h3>
								<ul className='space-y-3 text-xs text-gray-600'>
									{[
										'Thông báo Đào tạo',
										'Tuyển dụng & Việc làm',
										'Biểu mẫu Sinh viên',
										'Cổng Đăng ký tín chỉ'
									].map((item) => (
										<li key={item}>
											<a
												href='#'
												className='flex items-center gap-2 bg-gray-50 hover:bg-blue-50 hover:text-[#1f4c7a] text-gray-700 px-3 py-2.5 rounded-lg transition'>
												<span className='text-[#1f4c7a] font-bold'>
													›
												</span>{' '}
												{item}
											</a>
										</li>
									))}
								</ul>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
	)
}

export default HomePage
