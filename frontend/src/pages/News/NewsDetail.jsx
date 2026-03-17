import React, { useState, useEffect } from "react";
import { useParams, useLocation, useNavigate } from "react-router-dom";
import newsApi from "../../api/newsApi";
import { newsTypeLabel, newsTypeColor } from "../../constants/news";
import { formatDateTime, timeAgo } from "../../helpers/newsHelpers";
import PopupMessage from "../../components/parts/PopupMessage";
import Button from "../../components/parts/Button";

const NewsDetail = () => {
  const { id } = useParams();
  const location = useLocation();
  const navigate = useNavigate();
  const [popup, setPopup] = useState(null);
  const [news, setNews] = useState(location.state || null);
  const [relatedNews, setRelatedNews] = useState([]);
  const [comments, setComments] = useState([]);
  const [commentText, setCommentText] = useState("");
  const [error, setError] = useState(null);

  // Load bài viết
  useEffect(() => {
    if (!news) {
      newsApi
        .getById(id)
        .then(setNews)
        .catch(() => setError("Không tìm thấy bài viết"));
    }
  }, [id]);

  // Load comments
  useEffect(() => {
    newsApi
      .getComments(id)
      .then(setComments)
      .catch(() => {});
  }, [id]);

  // Load tin liên quan
  useEffect(() => {
    if (!news?.newsType) return;
    newsApi
      .search({ newsType: news.newsType, pageSize: 5 })
      .then((res) => {
        const data = res.items ?? res;
        setRelatedNews(data.filter((n) => n.newsID !== Number(id)).slice(0, 3));
      })
      .catch(() => {});
  }, [id, news?.newsType]);

  // Popup tự đóng
  useEffect(() => {
    if (!popup) return;
    const t = setTimeout(() => setPopup(null), 3000);
    return () => clearTimeout(t);
  }, [popup]);

  const handleComment = async () => {
    if (!commentText.trim()) return;
    try {
      await newsApi.postComment(id, { content: commentText });
      setCommentText("");
      const updated = await newsApi.getComments(id);
      setComments(updated);
    } catch (err) {
      const msg =
        err.response?.data?.message ||
        err.response?.data?.detail ||
        "Vui lòng đăng nhập để thực hiện chức năng bình luận";
      setPopup(msg);
    }
  };

  // Error state - giống ảnh 2
  if (error) {
    return (
      <div className="max-w-5xl mx-auto px-4 py-12">
        <Button link="/news" message="Trở về Trang chủ" />
        <div className="mt-6 flex flex-col lg:flex-row gap-8">
          <div className="flex-1 bg-white rounded-2xl border border-gray-100 p-16 flex flex-col items-center gap-4 text-center">
            <div className="text-red-400 text-5xl">🔗</div>
            <h3 className="text-xl font-semibold text-gray-800">
              Không tìm thấy bài viết
            </h3>
            <p className="text-gray-500 text-sm">
              Tin tức này hiện không còn tồn tại hoặc đã bị ẩn khỏi hệ thống.
            </p>
            <button
              onClick={() => navigate("/news")}
              className="mt-2 px-5 py-2 border border-gray-300 rounded-lg text-sm hover:bg-gray-50 transition"
            >
              🏠 Về trang chủ
            </button>
          </div>
          {/* Sidebar vẫn hiển thị */}
          <div className="w-full lg:w-72">
            <NewsSidebar relatedNews={relatedNews} navigate={navigate} />
          </div>
        </div>
      </div>
    );
  }

  if (!news)
    return (
      <div className="max-w-5xl mx-auto px-4 py-12 text-center text-gray-500">
        Đang tải bài viết...
      </div>
    );

  return (
    <div className="max-w-7xl mx-auto px-4 py-8">
      <Button link="/news" message="Trở về Trang chủ" />

      <div className="flex flex-col lg:flex-row gap-8 mt-4">
        {/* NỘI DUNG CHÍNH */}
        <div className="flex-1 bg-white rounded-xl shadow-sm border border-gray-100 p-8">
          {/* Badge */}
          <div className="mb-3">
            <span
              className={`text-xs font-semibold px-3 py-1 rounded-full ${newsTypeColor[news.newsType] || "bg-gray-100 text-gray-600"}`}
            >
              {newsTypeLabel[news.newsType] || news.newsType}
            </span>
          </div>

          {/* Tiêu đề */}
          <h1 className="text-3xl font-bold text-[#1f4c7a] leading-tight mb-4">
            {news.title}
          </h1>

          {/* Meta */}
          <div className="flex items-center gap-4 text-sm text-gray-500 border-b pb-4 mb-6 flex-wrap">
            <span>📅 {formatDateTime(news.createdAt)}</span>
            <span>👁 {news.viewCount} lượt xem</span>
            <span>✍️ Quản trị viên Khoa</span>
          </div>

          {/* Nội dung */}
          <div className="prose max-w-none text-gray-700 leading-relaxed whitespace-pre-wrap">
            {news.content}
          </div>

          {/* Bình luận - giống ảnh 1 */}
          <div className="mt-10 border-t pt-6">
            <h3 className="text-lg font-semibold text-gray-800 mb-4">
              💬 Bình luận ( {comments.length} )
            </h3>

            {/* Input bình luận */}
            <div className="flex gap-3 mb-6">
              <div className="w-9 h-9 rounded-full bg-gray-300 flex items-center justify-center text-white text-sm flex-shrink-0">
                👤
              </div>
              <div className="flex-1">
                <textarea
                  value={commentText}
                  onChange={(e) => setCommentText(e.target.value)}
                  placeholder="Nhập bình luận của bạn về bài viết này..."
                  className="w-full border border-gray-200 rounded-lg px-4 py-3 text-sm resize-none focus:outline-none focus:ring-2 focus:ring-[#1f4c7a] min-h-[80px]"
                />
                <div className="flex justify-end mt-2">
                  <button
                    onClick={handleComment}
                    className="flex items-center gap-2 bg-[#1f4c7a] text-white px-4 py-2 rounded-lg text-sm font-medium hover:bg-[#163a5d] transition"
                  >
                    ✈️ Gửi bình luận
                  </button>
                </div>
              </div>
            </div>

            {/* Danh sách bình luận */}
            <div className="space-y-4">
              {comments.map((c, i) => (
                <div key={i} className="flex gap-3">
                  <div className="w-9 h-9 rounded-full bg-[#1f4c7a] flex items-center justify-center text-white text-sm font-bold flex-shrink-0">
                    {c.studentName?.charAt(0) || "?"}
                  </div>
                  <div className="flex-1">
                    <div className="flex items-center gap-2 mb-1">
                      <span className="text-sm font-semibold text-gray-800">
                        {c.studentName}
                      </span>
                      <span className="text-xs text-gray-400">
                        {timeAgo(c.createdAt)}
                      </span>
                    </div>
                    <p className="text-sm text-gray-600">{c.content}</p>
                  </div>
                </div>
              ))}
            </div>
          </div>
        </div>

        {/* SIDEBAR */}
        <div className="w-full lg:w-72 flex-shrink-0">
          <NewsSidebar relatedNews={relatedNews} navigate={navigate} />
        </div>
      </div>

      {popup && <PopupMessage message={popup} onClose={() => setPopup(null)} />}
    </div>
  );
};

// Sidebar component tách riêng để dùng lại
const NewsSidebar = ({ relatedNews, navigate }) => (
  <div className="bg-white rounded-xl shadow-sm border border-gray-100 p-5 sticky top-4">
    <h3 className="font-semibold text-gray-800 mb-4 text-sm uppercase tracking-wide">
      Tin tức khác
    </h3>
    <div className="space-y-4">
      {relatedNews.length === 0 ? (
        <p className="text-sm text-gray-400">Không có tin liên quan.</p>
      ) : (
        relatedNews.map((item) => (
          <div
            key={item.newsID}
            onClick={() => navigate(`/news/${item.newsID}`, { state: item })}
            className="flex gap-3 cursor-pointer group"
          >
            <div className="w-14 h-14 rounded-lg bg-[#e8f0f9] flex items-center justify-center text-xl flex-shrink-0">
              📰
            </div>
            <div className="flex-1 min-w-0">
              <p className="text-sm font-medium text-gray-700 group-hover:text-[#1f4c7a] transition line-clamp-2">
                {item.title}
              </p>
              <p className="text-xs text-gray-400 mt-1">
                {timeAgo(item.createdAt)}
              </p>
            </div>
          </div>
        ))
      )}
    </div>
  </div>
);

export default NewsDetail;
